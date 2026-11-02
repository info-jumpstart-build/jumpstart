#!/bin/bash
DIR="$(cd "$(dirname "$0")" && pwd)"
if [[ "$OSTYPE" == "darwin"* ]]; then
    osascript -e "tell app \"Terminal\" to do script \"cd '$DIR/web' && npm run dev\""
else
    gnome-terminal -- bash -c "cd '$DIR/web' && npm run dev; exec bash" 2>/dev/null \
        || xterm -e "bash -c \"cd '$DIR/web' && npm run dev; exec bash\"" 2>/dev/null \
        || nohup bash -c "cd '$DIR/web' && npm run dev" > '$DIR/web.log' 2>&1 &
fi
