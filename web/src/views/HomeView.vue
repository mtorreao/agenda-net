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
  <div>
    <br />
    <Card>
      <template #content>
        <div class="title-card-wrapper" style="margin: -30px 0;">
          <h2>Agenda</h2>
          <Button label="Novo contato" icon="pi pi-plus" class="p-button-success mr-2" />
        </div>
      </template>
    </Card>
    <br />

    <div class="list-wrapper">
      <ContactCard v-for="contact in contacts" :key="contact.id" :contact="contact" />
    </div>
  </div>
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
