<script setup lang="ts">
import { useHead } from "@unhead/vue";

useHead({
  title: "FreeMessageBoard",
  meta: [
    {
      name: "description",
      content:
        "message board created by the community of enthusiasts for the same enthusiasts",
    },
  ],
});
</script>

<template>
  <div class="app">
    <AppLayout />
  </div>
</template>
