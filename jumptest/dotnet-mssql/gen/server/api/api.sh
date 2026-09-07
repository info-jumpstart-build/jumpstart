#!/bin/bash
DIR="$(cd "$(dirname "$0")" && pwd)"
if [[ "$OSTYPE" == "darwin"* ]]; then
    osascript -e "tell app \"Terminal\" to do script \"cd '$DIR/api' && dotnet api.dll\""
else
    gnome-terminal -- bash -c "cd '$DIR/api' && dotnet api.dll; exec bash" 2>/dev/null \
        || xterm -e "bash -c \"cd '$DIR/api' && dotnet api.dll; exec bash\"" 2>/dev/null \
        || nohup bash -c "cd '$DIR/api' && dotnet api.dll" > '$DIR/api.log' 2>&1 &
fi
