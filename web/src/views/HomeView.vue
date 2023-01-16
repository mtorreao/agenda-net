<script setup>
</script>

<template>
  <div class="container mt-4">
    <h1>Aplicativo Minha Agenda</h1>
    <p>
      Desenvolvido com Vue.js, VueRouter, Pinia e Vite.
    </p>
    <strong>Principais funcionalidades</strong>
    <ul>
      <li>Navegação entre telas</li>
      <li>Proteção de rotas autenticadas</li>
      <li>Login</li>
      <li>Cadastro</li>
      <li>Listagem de contatos</li>
      <li>Cadastro de contato</li>
      <li>Remoção de contato</li>
      <li>Utilização de store global com o Pinia</li>
    </ul>
    <router-link to="/contacts">Tela de contatos</router-link>

  </div>
</template>

<style scoped>

</style>
