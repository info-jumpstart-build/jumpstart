#!/bin/bash
# Apply the hand-written user seed files in usr/database/data, reusing the same
# ~/.jumptest.json configuration the generated data loader reads.
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
exec python3 "$SCRIPT_DIR/load-usr.py" "$@"
