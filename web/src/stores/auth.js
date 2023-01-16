import { defineStore } from "pinia";
import AuthService from "../services/auth-service";

const authService = AuthService.instance;

export const useAuthStore = defineStore("authStore", {
  state: () => ({
    authState: "not_logged",
    user: null,
  }),
  getters: {
    isLogged: (state) => state.authState === "logged",
    getUser: (state) => state.user,
  },
  actions: {
    login(email, password) {
      return authService.login(email, password).then((response) => {
        if (response.success === true) {
          this.authState = "logged";
          this.user = response.data.user;
        }
        return response;
      });
    },
    register({email, password, name}) {
      return authService.register({email, password, name}).then((response) => {
        if (response.success === true) {
          this.authState = "logged";
          this.user = response.data.user;
        }
        return response;
      });
    },
    logout() {
      authService.logout();
      this.authState = "not_logged";
    },
    checkLogin() {
      if (authService._getToken()) {
        this.authState = "logged";
      }
    },
  },
});
