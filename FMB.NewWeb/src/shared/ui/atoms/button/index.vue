<script setup lang="ts">
import type { QBtnProps } from "quasar";
import { QBtn } from "quasar";

export interface QuaButtonProps extends QBtnProps {
  classButton?: string[];
}

const props = defineProps<QuaButtonProps>();
</script>

<template>
  <QBtn
    v-bind="props"
    :ripple="false"
    :class="props.classButton ?? []"
    class="qua-button"
  >
    <slot />
  </QBtn>
</template>

<style lang="scss" scoped>
@import "./styles.module.scss";
</style>
