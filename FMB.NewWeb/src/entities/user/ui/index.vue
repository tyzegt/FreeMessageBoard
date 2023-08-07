<script setup lang="ts">
defineSlots<{
  user?: {};
  ipAddress?: {};
}>();
</script>

<template>
  <div>
    <slot name="user" />
    <slot name="ipAddress" />
  </div>
</template>
