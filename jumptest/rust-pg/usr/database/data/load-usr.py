#!/usr/bin/env python3
"""Apply the hand-written user seed files in usr/database/data.

Reads the same ~/.jumptest.json (or legacy ~/.jumptest) the generated loader
uses, then runs psql against every *.sql in this directory (sorted). Inserts in
those files are idempotent, so this is safe to run on every build.
"""
import glob
import json
import os
import subprocess
import sys
from pathlib import Path

HERE = Path(__file__).resolve().parent


def load_config():
    home = Path.home()
    json_file = home / ".jumptest.json"
    legacy_file = home / ".jumptest"
    cfg = {"hostname": "localhost", "port": "5432", "database": "jumptest",
           "username": "postgres", "password": ""}
    if json_file.exists():
        data = json.load(open(json_file))
        ds = data.get("datasources", {}).get("default", {})
        for k in ("hostname", "port", "database", "username", "password"):
            if ds.get(k):
                cfg[k] = str(ds[k])
    elif legacy_file.exists():
        parts = legacy_file.read_text().strip().split(":")
        if len(parts) >= 4:
            cfg["hostname"], cfg["port"], cfg["database"] = parts[1].strip(), parts[2].strip(), parts[3].strip()
        if len(parts) >= 5:
            cfg["username"] = parts[4].strip()
        if len(parts) >= 6:
            cfg["password"] = parts[5].strip()
    else:
        print(f"No ~/.jumptest.json or ~/.jumptest found; cannot seed.", file=sys.stderr)
        sys.exit(1)
    return cfg


def main():
    cfg = load_config()
    env = os.environ.copy()
    if cfg["password"]:
        env["PGPASSWORD"] = cfg["password"]

    files = sorted(glob.glob(str(HERE / "*.sql")))
    if not files:
        print("No user seed files to load.")
        return

    print(f"Seeding user data into {cfg['database']} @ {cfg['hostname']}:{cfg['port']}")
    for f in files:
        print(f"Loading: {os.path.basename(f)}")
        cmd = ["psql", f"--host={cfg['hostname']}", f"--port={cfg['port']}",
               f"--dbname={cfg['database']}", f"--username={cfg['username']}",
               "--variable=ON_ERROR_STOP=1", f"--file={f}"]
        result = subprocess.run(cmd, env=env)
        if result.returncode != 0:
            print(f"Failed to load: {f}", file=sys.stderr)
            sys.exit(result.returncode)


if __name__ == "__main__":
    main()
