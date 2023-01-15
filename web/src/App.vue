<script setup>
import { onMounted } from 'vue';
import { RouterLink, RouterView } from 'vue-router'
import { useToast } from "primevue/usetoast";

onMounted(() => {
  // const toast = useToast()
  // toast.add({
  //   severity: 'success',
  //   summary: 'Success Message',
  //   detail: 'Order submitted',
  //   life: 3000
  // })
});

</script>

<template>
  <header>
    <Menubar >
      <template #start>
        <RouterLink class="nav-item text-larger" to="/">Minha Agenda</RouterLink>
      </template>
      <template #end>
        <RouterLink class="nav-item" to="/sign-in">Login</RouterLink>
        <RouterLink class="nav-item" to="/sign-up">Cadastro</RouterLink>
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
