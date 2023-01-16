<script setup>
const props = defineProps({
  contact: {
    type: Object,
    required: true
  }
})

function cellphoneMask(value) {
  return value
      .replace(/\D/g, '')
      .replace(/(\d{2})(\d{1})(\d{4})(\d{4})/, "($1) $2 $3-$4")
  ;
}
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
      <p>{{ cellphoneMask(props.contact.phone) }}</p>
    </template>
    <template #footer>
      <Button
        icon="pi pi-pencil"
        class="p-button-rounded p-button-text"
        @click="$emit('onEdit')" />
      <Button
        icon="pi pi-trash"
        class="p-button-rounded p-button-text"
        @click="$emit('onRemove')" />
    </template>
  </Card>
</template>