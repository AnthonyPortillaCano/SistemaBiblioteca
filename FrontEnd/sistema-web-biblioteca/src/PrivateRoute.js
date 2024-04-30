import React from "react";
import { Route, Redirect } from "react-router-dom";
import userService from "./services/user-service";

const PrivateRoute = ({ component: Component, ...rest }) => (
  <Route
    {...rest}
    render={(props) =>
      userService.getCurrentUser() ? (
        <Component {...props} />
      ) : (
        <Redirect to={{ pathname: "/signin", state: { from: props.location } }} />
      )
    }
  />
);

export default PrivateRoute;