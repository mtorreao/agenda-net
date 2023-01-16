<script setup>
import { ref, computed } from "vue";

const props = defineProps({
  title: {
    type: String,
    required: true,
  },
  type: {
    type: String,
    required: false,
    default: "login",
  },
  onSubmit: {
    type: Function,
    required: true,
  },
});

const name = ref("");
const email = ref("");
const password = ref("");
const passwordConfirmation = ref("");
const isLoading = ref(false);
const errors = ref([]);

const nameErrors = computed(() => {
  return errors.value.filter((error) => error.key === "Name");
});

const emailErrors = computed(() => {
  return errors.value.filter((error) => error.key === "Email");
});

const passwordErrors = computed(() => {
  return errors.value.filter((error) => error.key === "Password");
});

const nameHasError = computed(() => {
  return errors.value.filter((error) => error.key === "Name").length > 0;
});

const emailHasError = computed(() => {
  return errors.value.filter((error) => error.key === "Email").length > 0;
});

const passwordHasError = computed(() => {
  return errors.value.filter((error) => error.key === "Password").length > 0;
});

const passwordConfirmationHasError = computed(() => {
  return password.value != passwordConfirmation.value;
});

function clearError(key) {
  errors.value = errors.value.filter((error) => error.key.toLowerCase() !== key.toLowerCase());
}

function submit() {
  if (props.type === 'register' && passwordConfirmationHasError.value) {
    return;
  }
  isLoading.value = true;
  props
    .onSubmit({
      email: email.value,
      password: password.value,
      name: name.value,
    })
    .then((validationErrors) => {
      if (validationErrors)
        errors.value = validationErrors;
    })
    .finally(() => {
      isLoading.value = false;
    });
}
</script>

<template>
  <form @submit.prevent="submit" class="full-width">
    <Card>
      <template #title>
        <div class="title-wrapper">
          {{ props.title }}
        </div>
      </template>
      <template #content>
        <div class="input-wrappers">
          <span v-if="props.type === 'register'" class="p-float-label">
            <InputText
              id="name"
              type="text"
              class="full-width"
              :class="{ 'p-invalid': nameHasError }"
              v-model="name"
              aria-describedby="name-help"
              @change="clearError('name')" />
            <label for="name">Nome</label>
            <small v-if="nameHasError" id="name-help" class="p-error">
              {{ nameErrors[0].message}}
            </small>
          </span>
          <span class="p-float-label">
            <InputText
              id="email"
              type="text"
              class="full-width"
              :class="{ 'p-invalid': emailHasError }"
              v-model="email"
              aria-describedby="email-help"
              @change="clearError('email')" />
            <label for="email">Email</label>
            <small v-if="emailHasError" id="email-help" class="p-error">{{ emailErrors[0].message }}</small>
          </span>
          <span class="p-float-label">
            <InputText
              id="password"
              type="password"
              class="full-width"
              v-model="password"
              :class="{ 'p-invalid': passwordHasError }"
              @change="clearError('password')"
              aria-describedby="password-help" />
            <label for="password">Senha</label>
            <small v-if="passwordHasError" id="password-help" class="p-error">{{ passwordErrors[0].message }}</small>
          </span>
          <span v-if="props.type === 'register'" class="p-float-label">
            <InputText
              id="passwordConfirmation"
              type="password"
              class="full-width"
              v-model="passwordConfirmation"
              :class="{ 'p-invalid': passwordConfirmationHasError }"
              @change="clearError('passwordConfirmation')" />
            <label for="passwordConfirmation">Confirme a senha</label>
            <small v-if="passwordConfirmationHasError" id="email-help" class="p-error">Senhas não conferem</small>
          </span>
        </div>
      </template>
      <template #footer class="buttons-wrapper">
        <Button
          v-if="!isLoading"
          type="submit"
          class="full-width"
          :label="props.type === 'register' ? 'Criar' : 'Entrar'" />
        <div v-if="isLoading" class="full-width">Loading...</div>
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
