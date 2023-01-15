import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import PrimeVue from 'primevue/config';
import ToastService from 'primevue/toastservice';

// Component imports
import Dialog from 'primevue/dialog';
import Menubar from 'primevue/menubar';
import Toast from 'primevue/toast';
import InputText from 'primevue/inputtext';
import Card from 'primevue/card';
import Button from 'primevue/button';
import Toolbar from 'primevue/toolbar';

// CSS imports
import './assets/main.css'

import 'primevue/resources/themes/md-light-deeppurple/theme.css'       //theme
import 'primevue/resources/primevue.min.css'                           //core css
import 'primeicons/primeicons.css'                                     //icons

const app = createApp(App)

app.use(router)
app.use(PrimeVue);
app.use(ToastService);

// Components
app.component('Dialog', Dialog);
app.component('Menubar', Menubar);
app.component('Toast', Toast);
app.component('InputText', InputText);
app.component('Card', Card);
app.component('Button', Button);
app.component('Toolbar', Toolbar);

app.mount('#app')
