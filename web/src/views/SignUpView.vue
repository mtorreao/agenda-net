<script setup>
import AuthForm from '@/components/AuthForm.vue'

function submit() {
  console.log(`SignUpPage -> submit -> email: ${email.value}, password: ${password.value}`)
}
</script>

<template>
  <div class="container-center">
    <AuthForm @onSubmit="submit" :title="'Cadastro'" :type="'register'" />
  </div>
</template>


