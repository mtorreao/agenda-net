<script setup>
import { ref, provide, onMounted } from 'vue'
import { useDialog } from 'primevue/usedialog';
import ContactCard from '@/components/ContactCard.vue'
import ContactForm from '@/components/ContactForm.vue'
import { useContactStore } from '../stores/contact'

const contactStore = useContactStore()

onMounted( () => {
  contactStore.findAll()
})

const dialog = useDialog()
const dialogRef = ref(null)

function showDialog(contact) {
  dialogRef.value = dialog.open(ContactForm, {
    props: {
      header: contact ? 'Editar Contato':'Novo Contato',
      modal: true,
    },
    data: contact ? contact : { },
  })
}

function deleteContact(contact) {
  contactStore.delete(contact)
}

provide('contactFormDialogRef', dialogRef)
</script>

<template>
  <div class="container-centralized">
    <br />
    <Card class="element-wrapper" >
      <template #content>
        <div class="title-card-wrapper" style="margin: -30px 0;">
          <h2>Agenda</h2>
          <Button label="Novo contato" icon="pi pi-plus" class="p-button-success mr-2" @click="showDialog()" />
        </div>
      </template>
    </Card>
    <br />

    <div class="list-wrapper element-wrapper">
      <ContactCard v-for="contact in contactStore.contacts" :key="contact.id" :contact="contact"
        @onEdit="showDialog(contact)" @onRemove="deleteContact(contact)" />
    </div>
  </div>
  <DynamicDialog />
</template>

<style scoped>
.title-card-wrapper {
  display: flex;
  flex-direction: row;
  align-items: center;
  justify-content: space-between;
}

.list-wrapper {
  display: flex;
  flex-direction: column;
  align-items: stretch;
  justify-content: flex-start;
  height: 100vh;
  gap: var(--inline-spacing);
}

.container-centralized {
  width: 100%;
  display: flex;
  flex-direction: column;
  align-items: center;
}

@media (min-width: 320px) {
  .element-wrapper {
    width: 300px;
  }
}

@media (min-width: 425px) {
  .element-wrapper {
    width: 400px;
  }
}

@media (min-width: 768px) {
  .element-wrapper {
    width: 450px;
  }
}
</style>
