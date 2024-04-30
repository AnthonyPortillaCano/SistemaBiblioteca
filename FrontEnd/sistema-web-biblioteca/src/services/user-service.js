import axios from "axios";

const API_URL = "https://localhost:7221/Api/Usuario/";

class AuthService {
  signup(email, clave) {
    console.log("email : ", email);

    return axios.post(API_URL + "Insertar", {
      email,
      clave
    });
  }

  signin(email, clave) {
    console.log("email : ", email);

    return axios
      .post(API_URL + "ValidarUsuario", {
        email,
        clave
      })
      .then(response => {
          localStorage.setItem("userAuth", response.data.token);
        return response.data.token;
      });
  }

  logout() {
    localStorage.removeItem("userAuth");
  }

  getCurrentUser() {
    var token= localStorage.getItem('userAuth');
    console.log(token);
    return token;
  }
}

export default new AuthService();
