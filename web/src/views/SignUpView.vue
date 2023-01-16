<script setup>
import AuthForm from '@/components/AuthForm.vue'
import { useToast } from 'primevue/usetoast';
import router from '../router';
import { useAuthStore } from "../stores/auth";

const authStore = useAuthStore();
const toast = useToast();

async function submit({ email, password, name }) {
  const {success, data} = await authStore.register({email, name, password});
  if (success === true) {
    toast.add({ severity: 'success', summary: 'Sucesso', detail: 'Registro realizado com sucesso!', life: 3000 });
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
    <AuthForm :onSubmit="submit" :title="'Cadastro'" :type="'register'" style="max-width: 400px;" />
  </div>
</template>


