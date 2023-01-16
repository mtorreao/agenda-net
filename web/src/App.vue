<script setup>
import { onMounted } from "vue";
import { RouterLink, RouterView, useRouter } from "vue-router";
import { useAuthStore } from "./stores/auth";

const authStore = useAuthStore();
const router = useRouter();

onMounted(async () => {
  authStore.checkLogin();
});

async function signOut() {
  authStore.logout();
  await router.push("/sign-in");
}
</script>

<template>
  <header>
    <Menubar>
      <template #start>
        <RouterLink class="nav-item text-larger" to="/">Minha Agenda</RouterLink>
      </template>
      <template #end>
        <RouterLink
          v-if="authStore.isLogged === false"
          class="nav-item"
          to="/sign-in">Login</RouterLink>
        <RouterLink
          v-if="authStore.isLogged === false"
          class="nav-item"
          to="/sign-up">Cadastro</RouterLink>
        <a
          v-if="authStore.isLogged === true"
          class="nav-item"
          href="#"
          @click="signOut">Sair</a>
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
