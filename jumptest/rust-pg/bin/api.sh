#!/bin/bash
# Launch the API server in its own terminal window. The binary and this script
# are copied side-by-side into the project bin/ folder by `make`.
#
# stdout+stderr are tee'd to bin/api.out so an early crash (e.g. before the
# logger's first file write, or a dyld/panic message that only goes to stderr)
# is always captured even if the terminal window closes.
DIR="$(cd "$(dirname "$0")" && pwd)"
if [[ "$OSTYPE" == "darwin"* ]]; then
    osascript -e "tell app \"Terminal\" to do script \"cd '$DIR' && ./api 2>&1 | tee api.out\""
else
    gnome-terminal -- bash -c "cd '$DIR' && ./api 2>&1 | tee api.out; exec bash" 2>/dev/null \
        || xterm -e "bash -c \"cd '$DIR' && ./api 2>&1 | tee api.out; exec bash\"" 2>/dev/null \
        || nohup bash -c "cd '$DIR' && ./api" > '$DIR/api.out' 2>&1 &
fi
