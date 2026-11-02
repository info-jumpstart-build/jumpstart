#!/bin/bash
# Launch the Scheduler server in its own terminal window. The binary and this
# script are copied side-by-side into the project bin/ folder by `make`.
DIR="$(cd "$(dirname "$0")" && pwd)"
if [[ "$OSTYPE" == "darwin"* ]]; then
    osascript -e "tell app \"Terminal\" to do script \"cd '$DIR' && ./scheduler\""
else
    gnome-terminal -- bash -c "cd '$DIR' && ./scheduler; exec bash" 2>/dev/null \
        || xterm -e "bash -c \"cd '$DIR' && ./scheduler; exec bash\"" 2>/dev/null \
        || nohup bash -c "cd '$DIR' && ./scheduler" > '$DIR/scheduler.out' 2>&1 &
fi
