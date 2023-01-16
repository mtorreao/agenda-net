<script setup>
import AuthForm from "@/components/AuthForm.vue";
import { useRouter } from "vue-router";
import { useAuthStore } from "../stores/auth";

const authStore = useAuthStore();
const router = useRouter();

async function submit({ email, password }) {
  const {success, data} = await authStore.login(email, password);
  if (success === true) {
    router.push("/");
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
