import { createWebHistory, createRouter } from "vue-router";

import Login from "./components/Login.vue";
import Home from "./components/Home.vue";
import Register from "./components/Register.vue";

const routes = [
  {
    path: "/home",
    name: "home",
    component: Home,
  },
  {
    path: "/login",
    component: Login,
  },
  {
    path: "/register",
    name: "register",
    component: Register,
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
