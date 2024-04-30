import React from "react";
import userService from "./services/user-service";
import { useHistory } from "react-router-dom";
const LogoutButton = () => {
    const history = useHistory();
  const handleLogout = () => {
    userService.logout();
    history.push("/signin");
  };
  const isLoggedIn = userService.getCurrentUser(); 
  return isLoggedIn ? (
    <button onClick={handleLogout}>Cerrar Sesión</button>
  ) : null;
};

export default LogoutButton;