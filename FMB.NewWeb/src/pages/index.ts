import { RouteRecordRaw } from "vue-router";
import { createRouter, createWebHistory } from "vue-router";
import { Home } from "./home";
import { Registration } from "./registration";
import { New } from "./new";
import { Top } from "./top";
import { Trending } from "./trending";

export const routes: Array<RouteRecordRaw> = [
  {
    path: "/",
    component: async () => Home,
    meta: { layout: "Default" },
  },
  {
    path: "/new",
    component: async () => New,
    meta: { layout: "Home" },
  },
  {
    path: "/top",
    component: async () => Top,
    meta: { layout: "Home" },
  },
  {
    path: "/trending",
    component: async () => Trending,
    meta: { layout: "Home" },
  },
  {
    path: "/hi/:username",
    component: async () => Home,
    meta: { layout: "Home" },
    props: true,
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export { router };
