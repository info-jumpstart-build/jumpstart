#!/bin/bash
# Launch the MCP server in its own terminal window. The binary and this script
# are copied side-by-side into the project bin/ folder by `make`.
#
# Defaults to the stdio transport (the way an MCP client normally launches
# this binary directly, in which case you would not run this script at all --
# it exists for the --transport http case, or for manually exercising stdio
# from a terminal). Pass arguments through, e.g. `./mcp.sh --transport http`.
#
# stdout+stderr are tee'd to bin/mcp.out so an early crash (e.g. before the
# logger's first file write, or a dyld/panic message that only goes to stderr)
# is always captured even if the terminal window closes.
DIR="$(cd "$(dirname "$0")" && pwd)"
ARGS="$*"
if [[ "$OSTYPE" == "darwin"* ]]; then
    osascript -e "tell app \"Terminal\" to do script \"cd '$DIR' && ./mcp $ARGS 2>&1 | tee mcp.out\""
else
    gnome-terminal -- bash -c "cd '$DIR' && ./mcp $ARGS 2>&1 | tee mcp.out; exec bash" 2>/dev/null \
        || xterm -e "bash -c \"cd '$DIR' && ./mcp $ARGS 2>&1 | tee mcp.out; exec bash\"" 2>/dev/null \
        || nohup bash -c "cd '$DIR' && ./mcp $ARGS" > "$DIR/mcp.out" 2>&1 &
fi
