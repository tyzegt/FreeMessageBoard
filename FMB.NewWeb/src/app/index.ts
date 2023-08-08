import { createApp } from "vue";
import { createPinia } from "pinia";
import App from "./App.vue";
import { registerLayouts } from "~/shared/ui/";
import { router } from "~/pages";
import { Quasar } from "quasar";
import { createHead } from "@unhead/vue";

const head = createHead();
const pinia = createPinia();
const app = createApp(App);

app.use(head);
app.use(router);
app.use(pinia);
app.use(Quasar, {
  plugins: {},
});

registerLayouts(app);

export default app;
