import { defineStore } from "pinia";
import ContactService from "../services/contact-service";

const contactService = ContactService.instance;

export const useContactStore = defineStore("contactStore", {
  state: () => ({
    contacts: [],
  }),
  getters: {
    getContacts: (state) => state.contacts,
  },
  actions: {
    async findAll() {
      const response = await contactService.findAll();
      this.contacts = response;
      return response;
    },
    async create(contact) {
      const response = await contactService.create(contact);
      this.findAll();
      return response;
    },
    async update(contact) {
      const response = await contactService.update(contact);
      return response;
    },
    async delete(contact) {
      const response = await contactService.delete(contact);
      return response;
    },
    async getById(id) {
      return contactService.getById(id);
    },
  },
});
