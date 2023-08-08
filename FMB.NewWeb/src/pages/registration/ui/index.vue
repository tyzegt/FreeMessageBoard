<script setup lang="ts">
import { SetName } from "~/features/user";
import { useRouter } from "vue-router";
import { storeToRefs } from "pinia";
import { useUserStore } from "~/entities/user/model";
import { QuaButton } from "~/shared/ui";

const userStore = useUserStore();

const { name } = storeToRefs(userStore);

const router = useRouter();
function go() {
  if (name.value) router.push(`/hi/${encodeURIComponent(name.value)}`);
}
</script>

<template>
  <SetName />
  <QuaButton @click="go()" label="go" :disable="!name" class="go-home" />
</template>

<style lang="scss" scoped>
@import "./styles.module.scss";
</style>
