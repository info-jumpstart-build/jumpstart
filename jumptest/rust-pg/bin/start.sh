#!/bin/bash
# Launch all backend components (api, scheduler, scriptagent, web) and leave
# a PID breadcrumb file behind for each one, so stop.sh can find and stop
# them later. This works whether a component ended up running in its own
# Terminal window (macOS/gnome-terminal/xterm) or backgrounded via nohup,
# because we don't rely on job control ($!) -- we just look, after starting
# each one, for a live process with the right name and working directory.
DIR="$(cd "$(dirname "$0")" && pwd)"
cd "$DIR"

# Print a process's current working directory, macOS and Linux alike.
_cwd_of() {
    local pid="$1"
    if [[ "$OSTYPE" == "darwin"* ]]; then
        lsof -p "$pid" -a -d cwd 2>/dev/null | awk 'NR==2 { for (i=9;i<=NF;i++) printf "%s ", $i; print "" }' | sed 's/ *$//'
    else
        readlink -f "/proc/$pid/cwd" 2>/dev/null
    fi
}

# Poll (up to ~10s) for processes named $1 whose cwd is $2, and write
# "PID TTY" (one pair per line) to $3. Used right after launching a
# component, since Terminal windows/gnome-terminal take a moment to
# actually start the command. The tty is recorded too (not just the pid)
# so stop.sh can later close the actual Terminal.app window/tab the
# process was running in, not just kill the process itself.
_capture_pids() {
    local name="$1" want_cwd="$2" pidfile="$3"
    local tries=0
    while [[ $tries -lt 20 ]]; do
        local found=""
        for pid in $(pgrep -x "$name" 2>/dev/null); do
            if [[ "$(_cwd_of "$pid")" == "$want_cwd" ]]; then
                local tty
                tty="$(ps -p "$pid" -o tty= 2>/dev/null | tr -d ' ')"
                found="${found}${pid} ${tty}"$'\n'
            fi
        done
        if [[ -n "$found" ]]; then
            printf '%s' "$found" > "$pidfile"
            return 0
        fi
        sleep 0.5
        tries=$((tries + 1))
    done
    rm -f "$pidfile"
    return 1
}

echo "Starting api..."
./api.sh
_capture_pids "api" "$DIR" "$DIR/api.pid" || echo "  warning: could not find api's pid"

echo "Starting scheduler..."
./scheduler.sh
_capture_pids "scheduler" "$DIR" "$DIR/scheduler.pid" || echo "  warning: could not find scheduler's pid"

echo "Starting scriptagent..."
./scriptagent.sh
_capture_pids "scriptagent" "$DIR" "$DIR/scriptagent.pid" || echo "  warning: could not find scriptagent's pid"

echo "Starting web..."
./web.sh
_capture_pids "node" "$DIR/web" "$DIR/web.pid" || echo "  warning: could not find web's pid"

echo "All components started. Run ./stop.sh to stop them."
