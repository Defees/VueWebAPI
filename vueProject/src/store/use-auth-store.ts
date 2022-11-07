import { defineStore } from "pinia";
import AuthService from "@/services/auth-service";

const user = JSON.parse(localStorage.getItem("user")!);
const initialState = user
  ? { status: { loggedIn: true }, user }
  : { status: { loggedIn: false }, user: null };

export const useAuthStore = defineStore("auth", () => {
  const state = initialState;

   async function login(user: any) {
    return await AuthService.login(user).then(
      (user) => {
        state.status.loggedIn = true;
        state.user = user;
        return Promise.resolve(user);
      },
      (error) => {
        state.status.loggedIn = false;
        state.user = null;
        return Promise.reject(error);
      }
    );
  }

  async function logout() {
   await AuthService.logout();
   state.status.loggedIn = false;
   state.user = null;
  }

  async function register(user: any) {
    return AuthService.register(user).then(
      (response) => {
        state.status.loggedIn = false;
        return Promise.resolve(response.data);
      },
      error => {
        state.status.loggedIn = false;
        return Promise.reject(error);
      }
    );
  }

  return {state, login, logout, register}
});
