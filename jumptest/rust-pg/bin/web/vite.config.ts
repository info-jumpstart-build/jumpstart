import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";

// jumptest -- generated Vite config. Dev server port matches the port
// used by the generated Blazor web project (see Properties/launchSettings.json
// in web-blazor) so existing bookmarks / Auth0 callback URLs keep working.
export default defineConfig({
  plugins: [react()],
  server: {
    port: 5063,
  },
  preview: {
    port: 5063,
  },
});
