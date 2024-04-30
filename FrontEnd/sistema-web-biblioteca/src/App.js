import React, { Component } from "react";
import { Switch, Route, Link } from "react-router-dom";
import "bootstrap/dist/css/bootstrap.min.css";
import "./App.css";

import userService from "./services/user-service";
import Signup from "./components/signup-component";
import Signin from "./components/signin-component";
import Libro from "./components/libro-component";
import eventListener from "./common/listener";
import PrivateRoute from "./PrivateRoute";
import LogoutButton  from "./LogoutButton";
class App extends Component {
  constructor(props) {
    super(props);
    this.logOut = this.logOut.bind(this);

    this.state = {
      currentUser: undefined
    };
  }

  logOut() {
    userService.logout();
    
  }

  componentDidMount() {
    const user = userService.getCurrentUser();
    if (user != null) {
      this.setState({
        currentUser: user
      });
    }
    eventListener.on("signout", () => {
      this.logOut();
    });
  }

  componentWillUnmount() {
    eventListener.remove("signout");
    this.setState({
      currentUser: undefined
    });
  }

  render() {

    return (
      <div>
        <nav className="navbar navbar-expand navbar-dark bg-primary">
          <Link to={"/"} className="navbar-brand">
            <b>Biblioteca de libros</b>
          </Link>
          <div className="navbar-nav mr-auto">
            <li className="nav-item">
              <Link to={"/signup"} className="nav-link">
              <b>Sign Up</b>
              </Link>
            </li>
            <li className="nav-item">
              <Link to={"/signin"} className="nav-link">
              <b>Sign In</b>
              </Link>
            </li>
            <li className="nav-item">
            <LogoutButton />
            </li>
          </div>
        </nav>

        <div className="container mt-3">
          <Switch>
            <Route exact path={["/", "/signin"]} component={Signin} />
            <Route exact path="/signin" component={Signin} />
            <Route exact path="/signup" component={Signup} />
            <PrivateRoute exact path="/libro" component={Libro} />
          </Switch>
        </div>

      </div>
    );
  }
}

export default App;
