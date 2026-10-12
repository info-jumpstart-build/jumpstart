// The Node/React analogue of Blazor's <AuthorizeRouteView>/<NotAuthorized>
// pair in App.razor: redirects unauthenticated users to Auth0 login instead
// of rendering the wrapped route.
import { useAuth0 } from "@auth0/auth0-react";
import type { ReactNode } from "react";

export default function ProtectedRoute({ children }: { children: ReactNode }) {
  const { isAuthenticated, isLoading, loginWithRedirect } = useAuth0();

  if (isLoading) {
    return <p>Checking authentication…</p>;
  }

  if (!isAuthenticated) {
    loginWithRedirect({ appState: { returnTo: window.location.pathname } });
    return null;
  }

  return <>{children}</>;
}
