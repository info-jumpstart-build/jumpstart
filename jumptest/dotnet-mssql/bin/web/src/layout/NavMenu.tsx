// Mirrors web/blazor/Layout/NavMenuLayout.razor.cshtml -- the sidebar showing
// child menu items for whichever top-level parent is currently selected in
// MainLayout. Auto-navigates to the first child link when the parent
// changes, same as the Blazor version's `shouldNavigate` logic.
import { useEffect, useRef, useState } from "react";
import { NavLink, useNavigate } from "react-router-dom";
import { useApiClient } from "../api/apiClient";
import type { NavMenu as NavMenuItem } from "../types/NavMenu";
import "./NavMenu.css";

interface NavMenuProps {
  selectedParentId: number | null;
}

export default function NavMenu({ selectedParentId }: NavMenuProps) {
  const api = useApiClient();
  const navigate = useNavigate();

  const [collapsed, setCollapsed] = useState(true);
  const [childMenuList, setChildMenuList] = useState<NavMenuItem[] | null>(null);
  const [selectedParentName, setSelectedParentName] = useState<string | null>(null);
  const previousParentId = useRef<number | null>(null);

  useEffect(() => {
    if (selectedParentId == null) {
      setChildMenuList(null);
      setSelectedParentName(null);
      previousParentId.current = null;
      return;
    }

    const parentChanged = previousParentId.current !== selectedParentId;
    let cancelled = false;

    Promise.all([
      api.get<NavMenuItem[]>(`/api/navmenu/byparent?parent_id=${selectedParentId}&orderby=ordinal`),
      api.get<NavMenuItem>(`/api/navmenu/${selectedParentId}`),
    ]).then(([children, parent]) => {
      if (cancelled) return;
      setChildMenuList(children);
      setSelectedParentName(parent?.name ?? null);

      if (parentChanged && children.length > 0 && children[0].link) {
        navigate(children[0].link);
      }
    });

    previousParentId.current = selectedParentId;
    return () => {
      cancelled = true;
    };
  }, [selectedParentId, api, navigate]);

  return (
    <div className={`${collapsed ? "collapse" : ""} nav-scrollable`} onClick={() => setCollapsed((c) => !c)}>
      <nav className="nav flex-column">
        {selectedParentId != null && selectedParentName && (
          <div className="nav-section-header px-3 py-2">
            <h6 className="text-muted mb-0">{selectedParentName}</h6>
          </div>
        )}

        {selectedParentId != null && childMenuList == null && (
          <p>
            <em>Loading menu items...</em>
          </p>
        )}

        {childMenuList != null && childMenuList.length > 0 && (
          <div className="nav-content">
            {childMenuList.map((child) => (
              <div className="nav-item px-3" key={child.id}>
                <NavLink className="nav-link" to={child.link}>
                  <span className="bi bi-list-nested-nav-menu" aria-hidden="true"></span> {child.name}
                </NavLink>
              </div>
            ))}
          </div>
        )}
      </nav>
    </div>
  );
}
