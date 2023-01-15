<script setup>
import { ref, provide } from 'vue'
import { useDialog } from 'primevue/usedialog';
import ContactCard from '@/components/ContactCard.vue'
import ContactForm from '@/components/ContactForm.vue'

const contacts = ref([
  { id: 1, name: 'John Doe', email: 'john@doe.com', phone: '81912323'}, 
  { id: 2, name: 'Jane Doe', email: 'jane@doe.com', phone: '81912323123'}, 
  { id: 3, name: 'John Smith', email: 'john@smith.com', phone: '81912323123'}, 
])

const dialog = useDialog()
const dialogRef = ref(null)

function showDialog(contact) {
  dialogRef.value = dialog.open(ContactForm, {
    props: {
      header: contact ? 'Editar Contato':'Novo Contato',
      modal: true,
    },
    data: contact ? contact : { }
  })

}
provide('contactFormDialogRef', dialogRef)
</script>

<template>
  <div>
    <br />
    <Card>
      <template #content>
        <div class="title-card-wrapper" style="margin: -30px 0;">
          <h2>Agenda</h2>
          <Button label="Novo contato" icon="pi pi-plus" class="p-button-success mr-2" @click="showDialog()" />
        </div>
      </template>
    </Card>
    <br />

    <div class="list-wrapper">
      <ContactCard v-for="contact in contacts" :key="contact.id" :contact="contact" />
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
</style>
