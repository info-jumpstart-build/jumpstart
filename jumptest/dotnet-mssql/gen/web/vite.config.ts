import { defineConfig, type Plugin, type PreviewServer, type ViteDevServer } from "vite";
import react from "@vitejs/plugin-react";
import { readFileSync } from "node:fs";
import { fileURLToPath } from "node:url";
import { dirname, resolve } from "node:path";

const currentDir = dirname(fileURLToPath(import.meta.url));

// config.json.cshtml (see public/config.json.cshtml) is generated into
// usr/web/public rather than this project's own public/ folder, so
// hand edits (Auth0 domain/clientId, apiBaseUrl) survive regeneration --
// the makefile copies it into bin/web/public at build time, but a plain
// `npm run dev` (or `npm run preview`) never runs the makefile and only
// serves this project's own public/ folder, so a request for
// /config.json 404s and Vite's SPA fallback serves index.html instead
// (the "Unexpected token '<', \"<!doctype \"..." JSON parse error). This
// plugin serves /config.json straight out of usr/web/public for both
// servers, mirroring what the makefile does for a built/deployed copy.
function serveUsrConfigPlugin(): Plugin {
  const usrConfigPath = resolve(currentDir, "./public/config.json");

  function middleware(req: { url?: string }, res: { setHeader: (name: string, value: string) => void; end: (chunk: unknown) => void }, next: () => void) {
    if (req.url === "/config.json") {
      try {
        res.setHeader("Content-Type", "application/json");
        res.end(readFileSync(usrConfigPath));
        return;
      } catch {
        // usr/web/public/config.json doesn't exist yet -- fall through so
        // Vite's normal 404 handling applies instead of a raw crash.
      }
    }
    next();
  }

  return {
    name: "serve-usr-config",
    configureServer(server: ViteDevServer) {
      server.middlewares.use(middleware);
    },
    configurePreviewServer(server: PreviewServer) {
      server.middlewares.use(middleware);
    },
  };
}

// jumptest -- generated Vite config. Dev server port matches the port
// used by the generated Blazor web project (see Properties/launchSettings.json
// in web-blazor) so existing bookmarks / Auth0 callback URLs keep working.
//
// strictPort: true makes a taken port a hard error instead of Vite's default
// of silently trying 5064, 5065, ... A stray/previous dev server (e.g. one
// started outside start.sh/stop.sh's PID tracking, so nothing stops it) can
// otherwise keep squatting on 5063 while a freshly started server quietly
// moves elsewhere -- the browser, still pointed at 5063 out of habit, then
// keeps talking to the stale process indefinitely. Mirrors the same
// fail-loud-on-conflict choice already made for the Rust/​.NET API,
// Scheduler, and ScriptAgent servers (see docs/operations.md).
export default defineConfig({
  plugins: [react(), serveUsrConfigPlugin()],
  server: {
    port: 5063,
    strictPort: true,
  },
  preview: {
    port: 5063,
    strictPort: true,
  },
});
