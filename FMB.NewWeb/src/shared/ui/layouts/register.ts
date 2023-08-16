import { App } from "vue";
import AppLayout from "./AppLayout.vue";
import DefaultLayout from "./DefaultLayout.vue";
import HomeLayout from "./HomeLayout.vue";

const layouts = [AppLayout, DefaultLayout, HomeLayout];

/**
 * Register layouts in the app instance
 *
 * @param {App<Element>} app
 */
export function registerLayouts(app: App<Element>) {
  // const layouts = import.meta.glob<ModuleNamespace>("./*.vue", { eager: true });
  // const layouts = import.meta.globEager<string, ModuleNamespace>("./*.vue");

  app.component("AppLayout", AppLayout);
  app.component("DefaultLayout", DefaultLayout);
  app.component("HomeLayout", HomeLayout);
  // Object.entries(layouts).forEach(([, layout]) => {
  //   app.component(, layout?.default);
  // });
}
