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
    <router-link to="/top">top</router-link><br />
    <router-link to="/trending">trending</router-link><br />
    <router-link to="/new">new</router-link>
</template>

<style lang="scss">
@import "./styles.module.scss";
</style>
