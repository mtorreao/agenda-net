<script setup>
import { onMounted, ref } from "vue";
import { RouterLink, RouterView } from "vue-router";
import router from "./router";
import { useAuthStore } from "./stores/auth";

const authStore = useAuthStore();

onMounted(async () => {
  authStore.checkLogin();
});

async function signOut() {
  authStore.logout();
  router.push("/sign-in");
}

const menuItems = ref([
  {
    label: "Home",
    icon: "pi pi-fw pi-home",
    to: "/",
  },
  {
    label: "Contatos",
    icon: "pi pi-fw pi-users",
    to: "/contacts",
    visible: () => authStore.isLogged,
  },
  {
    label: "Sair",
    icon: "pi pi-fw pi-sign-out",
    visible: () => authStore.isLogged,
    command: () => signOut(),
  },
  {
    label: "Login",
    icon: "pi pi-fw pi-sign-in",
    visible: () => !authStore.isLogged,
    to: "/sign-in",
  },
  {
    label: "Cadastro",
    icon: "pi pi-fw pi-user-plus",
    visible: () => !authStore.isLogged,
    to: "/sign-up",
  },
])
</script>

<template>
  <header>
    <Menubar :model="menuItems">
      <template #start>
        <RouterLink class="nav-item text-larger" to="/">Minha Agenda</RouterLink>
      </template>
      <template #end >
        <h5 v-if="authStore.isLogged === true && authStore.getUser?.name" class="nav-item">Olá, {{ authStore.getUser.name }}</h5>
      </template>
    </Menubar>
  </header>

  <Toast />

  <RouterView />
</template>

<style scoped>
.nav-item {
  text-decoration: none;
  color: var(--text-color);
  font-size: larger;
  margin: 0 10px;
  font-weight: bold;
}

.nav-item:hover {
  color: var(--text-color-secondary);
}

.text-larger {
  font-size: x-large;
}
</style>
