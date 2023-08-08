<script setup lang="ts">
import type { QInputProps } from "quasar";
import { QInput } from "quasar";

export interface QuaInputProps extends QInputProps {
  classInput?: string[];
  classWrapper?: string[];
}

const props = defineProps<QuaInputProps>();
</script>

<template>
  <QInput
    v-bind="props"
    :class="props.classWrapper ?? []"
    :input-class="props.classInput ?? []"
  >
    <slot />
  </QInput>
</template>

<style lang="scss" scoped></style>
