import { createFetch } from "@vueuse/core";

const http = createFetch({
  fetchOptions: {},
});

export { http };
