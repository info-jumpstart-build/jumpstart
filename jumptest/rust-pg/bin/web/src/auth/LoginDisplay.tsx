// Mirrors web/blazor/Auth/LoginDisplay.razor.cshtml.
import { useAuth0 } from "@auth0/auth0-react";

export default function LoginDisplay() {
  const { user, isAuthenticated, logout } = useAuth0();

  if (!isAuthenticated) {
    // ProtectedRoute already redirects unauthenticated users to Auth0 login,
    // so there is no "Log in" button to show here (unlike the Blazor
    // version, which renders one because unauthenticated content is
    // reachable before the redirect completes).
    return null;
  }

  return (
    <>
      <span className="me-3 text-secondary small">{user?.name}</span>
      <button
        className="btn btn-sm btn-outline-secondary"
        onClick={() => logout({ logoutParams: { returnTo: window.location.origin } })}
      >
        Log out
      </button>
    </>
  );
}
