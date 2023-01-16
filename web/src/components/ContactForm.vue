<script setup>
import { inject, onMounted, ref, watch } from "vue";
const props = defineProps({
  contact: {
    type: Object,
    required: false,
    default: { id: null, name: "", email: "", phone: "" },
  },
});

const isEdit = ref(props.contact.id ? true : false);
const contact = ref(props.contact);

function submit() {
  console.log(`ContactForm -> submit ->`, contact.value);
}

onMounted(() => {
  const dialogRef = inject("contactFormDialogRef");
  const dialogData = dialogRef?.value?.data;

  if (dialogData && dialogData.id) {
    contact.value = dialogData;
  }
});

watch(
  () => contact.value,
  (newVal) => {
    isEdit.value = newVal.id ? true : false;
  }
);
</script>

<template>
  <div class="container" style="padding-top: 5px">
    <form @submit.prevent="submit" class="form-wrapper">
      <span class="p-float-label">
        <InputText
          id="name"
          type="text"
          class="full-width"
          v-model="contact.name" />
        <label for="name">Nome</label>
      </span>
      <span class="p-float-label">
        <InputText
          id="email"
          type="text"
          class="full-width"
          v-model="contact.email" />
        <label for="email">Email</label>
      </span>
      <span class="p-float-label">
        <InputMask
          id="phone"
          type="text"
          class="full-width"
          mask="(99) 9 9999-9999"
          unmask
          v-model="contact.phone" />
        <label for="phone">Telefone</label>
      </span>
      <Button
        type="submit"
        class="full-width"
        :label="isEdit === false ? 'Criar' : 'Atualizar'" />
    </form>
  </div>
</template>

<style scoped>
.form-wrapper {
  display: flex;
  flex-direction: column;
  gap: var(--inline-spacing);
}
</style>
