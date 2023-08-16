<script setup lang="ts">
</script>

<template>
  <div class="grid-container">
    <div class="card--1">
      <slot name="card1" />
    </div>
    <div class="card--2">
      <slot name="card2" />
    </div>
    <div class="card--3">
      <slot name="card3" />
    </div>
    <div class="card--4">
      <slot name="card4" />
    </div>
  </div>
</template>

<style lang="scss" scoped>
.grid-container {
  --at-apply:   grid h-full w-full
                grid-cols-1 grid-rows-4 gap-2
                sm:gap-4
                md:grid-flow-col md:grid-rows-5 md:grid-cols-4 md:gap-6
                lg:grid-flow-row lg:grid-rows-4 lg:gap-8
                xl:grid-flow-col xl:grid-cols-3;
}
.card {
    &--1 {
        --at-apply: md:col-span-4 md:row-span-2 md:-order-2
                    lg:col-span-3 lg:-order-1
                    xl:col-auto xl:row-span-3 xl:-order-2;
    }

    &--2 {
        --at-apply: md:order-3 md:col-span-4 md:row-span-1
                    lg:col-span-1 lg:row-span-2 lg:-order-2
                    xl:row-span-1 xl:-order-1;
    }

    &--3 {
        --at-apply: md:order-2 md:col-span-2 md:row-span-2;
    }

    &--4 {
        --at-apply: md:order-3 md:col-span-2 md:row-span-2;
    }
}
</style>
