import { createApp } from 'vue'
import { createPinia } from 'pinia'
import App from './App.vue'
import router from "./router";
import "bootstrap";
import "bootstrap/dist/css/bootstrap.min.css";
import { FontAwesomeIcon } from './plugins/font-awesome'

const pinia = createPinia()
const app = createApp(App)

app.use(router)
app.use(pinia)
app.component("font-awesome-icon", FontAwesomeIcon)
app.mount('#app')

