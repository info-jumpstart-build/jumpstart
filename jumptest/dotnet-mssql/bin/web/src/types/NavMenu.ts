// Mirrors the JSON shape returned by GET /api/navmenu/... on both the
// dotnet and rust REST API servers (columns from core.nav_menu, snake_case
// as-is, no camelCase re-mapping).
export interface NavMenu {
  id: number;
  parent_id: number;
  ordinal: number;
  name: string;
  link: string;
}
