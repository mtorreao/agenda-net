import { createPinia } from "pinia";
import PrimeVue from "primevue/config";
import DialogService from "primevue/dialogservice";
import ToastService from "primevue/toastservice";
import { createApp } from "vue";
import App from "./App.vue";
import router from "./router";

// Component imports
import Button from "primevue/button";
import Card from "primevue/card";
import Dialog from "primevue/dialog";
import DynamicDialog from "primevue/dynamicdialog";
import InputMask from "primevue/inputmask";
import InputText from "primevue/inputtext";
import Menubar from "primevue/menubar";
import Toast from "primevue/toast";
import Toolbar from "primevue/toolbar";

// CSS imports
import "./assets/main.css";

import "primeicons/primeicons.css"; //icons
import "primevue/resources/primevue.min.css"; //core css
import "primevue/resources/themes/md-light-deeppurple/theme.css"; //theme

const app = createApp(App);
const pinia = createPinia();

app.use(pinia);
app.use(router);
app.use(PrimeVue);
app.use(ToastService);
app.use(DialogService);

// Global Components
app.component("Dialog", Dialog);
app.component("Menubar", Menubar);
app.component("Toast", Toast);
app.component("InputText", InputText);
app.component("Card", Card);
app.component("Button", Button);
app.component("Toolbar", Toolbar);
app.component("DynamicDialog", DynamicDialog);
app.component("InputMask", InputMask);

app.mount("#app");
