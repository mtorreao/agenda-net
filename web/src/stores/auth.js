import { defineStore } from "pinia";
import AuthService from "../services/auth-service";

const authService = AuthService.instance();

export const useAuthStore = defineStore("authStore", {
  state: () => ({
    authState: "not_logged",
  }),
  getters: {
    isLogged: (state) => state.authState === "logged",
  },
  actions: {
    login(email, password) {
      return authService.login(email, password).then((response) => {
        if (response.success === true) {
          this.authState = "logged";
        }
        return response;
      });
    },
    register({email, password, name}) {
      return authService.register({email, password, name}).then((response) => {
        if (response.success === true) {
          this.authState = "logged";
        }
        return response;
      });
    },
    logout() {
      authService.logout();
      this.authState = "not_logged";
    },
    checkLogin() {
      const isAuthenticated = authService.isAuthenticated();
      this.authState = isAuthenticated ? "logged" : "not_logged";
    },
  },
});
