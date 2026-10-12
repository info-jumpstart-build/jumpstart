// Mirrors web/blazor/Auth/Authentication.razor.cshtml's "/authentication/{action}"
// route. The Auth0 React SDK completes the code exchange itself (via
// onRedirectCallback wired up in main.tsx); this route just needs to exist
// so /authentication/callback is a valid client-side route while that
// exchange is in flight, and to surface any error.
import { useAuth0 } from "@auth0/auth0-react";
import { Navigate } from "react-router-dom";

export default function AuthCallback() {
  const { isLoading, error } = useAuth0();

  if (error) {
    return <p className="text-danger">Authentication error: {error.message}</p>;
  }

  if (isLoading) {
    return <p>Checking authentication…</p>;
  }

  return <Navigate to="/" replace />;
}
