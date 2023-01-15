<script setup>
  import { ref } from 'vue';

  const props = defineProps({
    onSubmit: {
      type: Function,
      required: true
    },
    title: {
      type: String,
      required: true
    }
  })

  const email = ref('')
  const password = ref('')
</script>

<template>
  <form @submit.prevent="props.onSubmit" class="full-width">
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
        </div>
      </template>
      <template #footer class="buttons-wrapper">
        <Button type="submit" class="full-width" label="Salvar" />
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

.full-width {
  width: 100%;
  max-width: 400px;
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