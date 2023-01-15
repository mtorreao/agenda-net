<script setup>
import { ref } from 'vue'
import ContactCard from '@/components/ContactCard.vue'

const contacts = ref([
  { id: 1, name: 'John Doe', email: 'john@doe.com', phone: '81912323'}, 
  { id: 2, name: 'Jane Doe', email: 'jane@doe.com', phone: '81912323123'}, 
  { id: 3, name: 'John Smith', email: 'john@smith.com', phone: '81912323123'}, 
])

</script>

<template>
  <div style="padding-bottom: 30px;">
    <br />
    <Toolbar class="mb-4">
      <template #start>
        <h3>Agenda</h3>
      </template>
      <template #end>
        <Button label="Novo contato" icon="pi pi-plus" class="p-button-success mr-2" />
      </template>
    </Toolbar>
    <br />

    <div class="list-wrapper">
      <ContactCard v-for="contact in contacts" :key="contact.id" :contact="contact" />
    </div>
  </div>
</template>

<style scoped>
.list-wrapper {
  display: flex;
  flex-direction: column;
  align-items: stretch;
  justify-content: flex-start;
  height: 100vh;
  gap: var(--inline-spacing);
}
</style>
