<script setup lang="ts">
import { storeToRefs } from "pinia";
import { useUserStore } from "~/entities/user/model";
import { QuaInput } from "~/shared/ui";

const userStore = useUserStore();

const { name } = storeToRefs(userStore);
</script>

<template>
  <div>
    <QuaInput
      v-model="name"
      :class-input="['setter-input']"
      placeholder="What's your name?"
    />
  </div>
</template>

<style lang="scss">
@import "./styles.module.scss";
</style>
