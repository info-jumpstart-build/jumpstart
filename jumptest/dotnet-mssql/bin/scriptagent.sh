#!/bin/bash
DIR="$(cd "$(dirname "$0")" && pwd)"
if [[ "$OSTYPE" == "darwin"* ]]; then
    osascript -e "tell app \"Terminal\" to do script \"cd '$DIR/scriptagent' && dotnet scriptagent.dll\""
else
    gnome-terminal -- bash -c "cd '$DIR/scriptagent' && dotnet scriptagent.dll; exec bash" 2>/dev/null \
        || xterm -e "bash -c \"cd '$DIR/scriptagent' && dotnet scriptagent.dll; exec bash\"" 2>/dev/null \
        || nohup bash -c "cd '$DIR/scriptagent' && dotnet scriptagent.dll" > '$DIR/scriptagent.log' 2>&1 &
fi
