<script setup lang="ts">
import { UserCard } from "~/entities/user";
import { useTechMetaStore } from "~/entities/tech-meta";
import { onBeforeMount } from "vue";
import { storeToRefs } from "pinia";

const props = defineProps<{
  username: string;
}>();

const techMetaStore = useTechMetaStore();

const { ip } = storeToRefs(techMetaStore);

const { fetchIp } = techMetaStore;

onBeforeMount(() => {
  fetchIp();
});
</script>

<template>
  <div i-twemoji:waving-hand class="greet-icon" />
  <UserCard class="user-card">
    <template #user>
      <div>{{ props.username }}!</div>
    </template>
    <template #ipAddress>
      <div>IP Address: {{ ip }}</div>
    </template>
  </UserCard>
</template>

<style lang="scss">
@import "./styles.module.scss";
</style>
