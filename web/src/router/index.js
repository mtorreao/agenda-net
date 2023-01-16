import { createRouter, createWebHistory } from "vue-router";
import HomeView from "../views/HomeView.vue";
import { useAuthStore } from "../stores/auth";

function userIsAuthenticated(to, from, next) {
  const authStore = useAuthStore();
  authStore.checkLogin();

  if (authStore.isLogged === true) next();
  else
    next({
      name: "sign-in",
    });
}

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: "/",
      name: "home",
      component: HomeView,
    },
    {
      path: "/contacts",
      name: "contacts",
      beforeEnter: [userIsAuthenticated],
      component: () => import("../views/ContactView.vue"),
    },
    {
      path: "/sign-in",
      name: "sign-in",
      component: () => import("../views/SignInView.vue"),
    },
    {
      path: "/sign-up",
      name: "sign-up",
      component: () => import("../views/SignUpView.vue"),
    },
  ],
});

export default router;
