<script setup>
  import { ref } from 'vue';

  const props = defineProps({
    title: {
      type: String,
      required: true
    },
    type: {
      type: String,
      required: false,
      default: 'login'
    }
  })

  const email = ref('')
  const password = ref('')
  const passwordConfirmation = ref('')
</script>

<template>
  <form @submit.prevent="$emit('onSubmit')" class="full-width">
    <Card>
      <template #title>
        <div class="title-wrapper">
          {{ props.title }}
        </div>
      </template>
      <template #content>
        <div class="input-wrappers ">
          <span class="p-float-label ">
            <InputText id="email" type="text" class="full-width" v-model="email" />
            <label for="email">Email</label>
          </span>
          <span class="p-float-label">
            <InputText id="password" type="password" class="full-width" v-model="password" />
            <label for="password">Senha</label>
          </span>
          <span v-if="props.type === 'register'" class="p-float-label">
            <InputText id="passwordConfirmation" type="password" class="full-width" v-model="passwordConfirmation" />
            <label for="passwordConfirmation">Confirme a senha</label>
          </span>
        </div>
      </template>
      <template #footer class="buttons-wrapper">
        <Button type="submit" class="full-width" :label="props.type === 'register' ? 'Criar' : 'Entrar'" />
      </template>
    </Card>
  </form>
</template>

<style scoped>
.input-wrappers {
  display: flex;
  flex-direction: column;
  gap: 1rem;
}

.buttons-wrapper {
  display: flex;
  flex-direction: row;
  justify-content: center;
}

.title-wrapper {
  display: flex;
  flex-direction: row;
  justify-content: center;
}
</style>