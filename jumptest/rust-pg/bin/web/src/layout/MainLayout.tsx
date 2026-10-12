// Mirrors web/blazor/Layout/MainLayout.razor.cshtml: the app shell. Fetches
// the top-level (parent_id=0) nav menu, renders it as a row of pill buttons,
// and hands the currently-selected parent down to <NavMenu> for the sidebar.
import { useEffect, useState, type ReactNode } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth0 } from "@auth0/auth0-react";
import { useApiClient } from "../api/apiClient";
import type { NavMenu as NavMenuItem } from "../types/NavMenu";
import NavMenu from "./NavMenu";
import LoginDisplay from "../auth/LoginDisplay";
import "./MainLayout.css";

interface MainLayoutProps {
  children: ReactNode;
}

export default function MainLayout({ children }: MainLayoutProps) {
  const navigate = useNavigate();
  const { isAuthenticated } = useAuth0();
  const api = useApiClient();

  // Typed via the initial value's "as" cast rather than useState<T>(...) --
  // a bare `Identifier<...>` outside a `<text>`-wrapped code region can make
  // RazorLight's markup parser mistake it for an HTML/JSX tag opening, which
  // silently breaks evaluation of the jumptest expression further down
  // in this file (this template has no `` code block, so it's all
  // "markup" mode from RazorLight's point of view).
  const [navMenuList, setNavMenuList] = useState(null as NavMenuItem[] | null);
  const [isMenuLoading, setIsMenuLoading] = useState(false);
  const [menuError, setMenuError] = useState(null as string | null);
  const [selectedParent, setSelectedParent] = useState(null as string | null);
  const [selectedParentId, setSelectedParentId] = useState(null as number | null);

  useEffect(() => {
    if (!isAuthenticated) return;

    let cancelled = false;
    setIsMenuLoading(true);
    setMenuError(null);

    api
      .get("/api/navmenu/byparent?parent_id=0&orderby=ordinal")
      .then((data) => {
        if (!cancelled) setNavMenuList(data as NavMenuItem[]);
      })
      .catch((err: Error) => {
        if (!cancelled) setMenuError(`Menu error: ${err.message}`);
      })
      .finally(() => {
        if (!cancelled) setIsMenuLoading(false);
      });

    return () => {
      cancelled = true;
    };
  }, [isAuthenticated, api]);

  function selectParent(navMenu: NavMenuItem) {
    setSelectedParent(navMenu.name);
    setSelectedParentId(navMenu.id);
  }

  return (
    <div className="page">
      <div className="sidebar">
        <div className="top-row ps-3 navbar navbar-dark">
          <div className="container-fluid">
            <a className="navbar-brand" href="/">
              jumptest
            </a>
          </div>
        </div>

        <NavMenu selectedParentId={selectedParentId} />
      </div>

      <main>
        {/* Parent Navigation Menu - Top Row */}
        <div className="top-row px-4">
          <nav className="nav nav-pills">
            <button className="nav-link" onClick={() => navigate("/")}>
              <span className="bi bi-house-door-fill" aria-hidden="true"></span> Home
            </button>
            {!isAuthenticated ? null : isMenuLoading ? (
              <p>
                <em>Loading menu...</em>
              </p>
            ) : menuError ? (
              <p className="text-danger small">{menuError}</p>
            ) : (
              navMenuList?.map((navMenu) => (
                <div className="nav-item" key={navMenu.id}>
                  <button
                    className={`nav-link ${selectedParent === navMenu.name ? "active" : ""}`}
                    onClick={() => selectParent(navMenu)}
                  >
                    <span className="bi bi-folder-fill" aria-hidden="true"></span> {navMenu.name}
                  </button>
                </div>
              ))
            )}
          </nav>

          <LoginDisplay />
        </div>

        <article className="content px-4">{children}</article>
      </main>
    </div>
  );
}
