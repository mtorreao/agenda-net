<script setup>
import AuthForm from "@/components/AuthForm.vue";
import router from '../router';
import { useAuthStore } from "../stores/auth";
import { useToast } from "primevue/usetoast";

const authStore = useAuthStore();
const toast = useToast();

async function submit({ email, password }) {
  const {success, message, data} = await authStore.login(email, password);
  if (success === true) {
    toast.add({ severity: 'success', summary: 'Sucesso', detail: 'Login com sucesso!', life: 3000 });
    router.push("/contacts");
    return;
  } else if (data.length === 0 && message) {
    toast.add({
      severity: "error",
      summary: "Erro",
      detail: message,
      life: 5000,
    });
    return;
  } else {
    return data; // validation errors
  }
}
</script>

<template>
  <div class="container-center">
    <AuthForm :onSubmit="submit" :title="'Login'" style="max-width: 400px" />
  </div>
</template>
