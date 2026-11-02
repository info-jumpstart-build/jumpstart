#!/bin/bash
# PostgreSQL Database Build Script Wrapper
# This script calls the Python build script for robust JSON parsing

# Get the directory where this script is located
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
PYTHON_SCRIPT="$SCRIPT_DIR/build.py"

# Check if Python 3 is available
if ! command -v python3 &> /dev/null; then
    echo "Error: Python 3 is required but not installed." >&2
    echo "Please install Python 3 and try again." >&2
    exit 1
fi

# Check if the Python script exists
if [ ! -f "$PYTHON_SCRIPT" ]; then
    echo "Error: Python build script not found at: $PYTHON_SCRIPT" >&2
    exit 1
fi

# Make the Python script executable if it isn't already
chmod +x "$PYTHON_SCRIPT" 2>/dev/null

# Execute the Python script with all arguments passed through
echo "PostgreSQL Database Build Script"
echo "================================"
echo ""

# Pass all command line arguments to the Python script
exec python3 "$PYTHON_SCRIPT" "$@"
