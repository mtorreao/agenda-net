<script setup>
import { inject, onMounted, ref, watch } from "vue";
import { useContactStore } from '../stores/contact'

const dialogRef = inject("contactFormDialogRef");
const props = defineProps({
  contact: {
    type: Object,
    required: false,
    default: { id: null, name: "", email: "", phone: ""},
  },
});

const contactStore = useContactStore()
const isEdit = ref(props.contact.id ? true : false);
const contact = ref(props.contact);

function resetForm() {
  contact.value.id = null;
  contact.value.name = '';
  contact.value.email = '';
  contact.value.phone = '';
}

async function submit() {
  try {
    if (isEdit.value) {
      await contactStore.update(contact.value);
    } else {
      await contactStore.create(contact.value);
    }
    resetForm();
    dialogRef.value.close()
  } catch (error) {
    console.error(error);
  }
}

onMounted(async () => {  
  const dialogData = dialogRef?.value?.data;

  resetForm();

  if (dialogData && dialogData.id) {
    const contactData = await contactStore.getById(dialogData.id)
    contact.value.id = contactData.id;
    contact.value.name = contactData.name;
    contact.value.email = contactData.email;
    contact.value.phone = contactData.phone;
    isEdit.value = true;
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
