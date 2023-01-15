<script setup>
const props = defineProps({
  contact: {
    type: Object,
    required: true
  }
})
</script>

<template>
  <Card>
    <template #title>
      {{ props.contact.name }}
    </template>
    <template #content>
      <strong>Email: </strong>
      <p>{{ props.contact.email }}</p>
      <strong>Telefone: </strong>
      <p>{{ props.contact.phone }}</p>
    </template>
  </Card>
</template>