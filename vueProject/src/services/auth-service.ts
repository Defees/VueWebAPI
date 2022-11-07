import { UserLoginModel } from "@/types/user-login-model";
import { UserRegisterModel } from "@/types/user-register-model";
import axios from "axios";

const API_AUTH_URL = "api/Authenticate/";

class AuthService {
    login(user: UserLoginModel) {
    return axios
      .post(API_AUTH_URL + "login", {
        'email': `${user.email}`,
        'password': `${user.password}`,
      })
      .then((response) => {
        if (response.data.accessToken && response.data.refreshToken) {
          localStorage.setItem("user", JSON.stringify(response.data));
        }

        return response.data;
      });
  }
  logout()  {
    return axios
    .post(API_AUTH_URL + "logOut", {})
    .then((response) => {
      if (response.data) {
        localStorage.removeItem("user");
      }
    });
  }
  register(user: UserRegisterModel) {
    console.log(user);
    return axios.post(API_AUTH_URL + 'register', {
        'email': `${user.email}`,
        'password':  `${user.password}`,
        'confirmPassword': `${ user.confirmPassword}`
    });
  }
}

export default new AuthService();