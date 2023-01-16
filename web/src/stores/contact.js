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
      if (response.success === true) {
        this.contacts.push(response.data);
      }
      return response;
    },
    async update(contact) {
      const response = await contactService.update(contact);
      if (response.success === true) {
        const index = this.contacts.findIndex((c) => c.id === contact.id);
        this.contacts[index] = response.data;
      }
      return response;
    },
    async delete(contact) {
      const response = await contactService.delete(contact);
      if (response.success === true) {
        this.contacts = this.contacts.filter((c) => c.id !== contact.id);
      }
      return response;
    },
    async getById(id) {
      return contactService.getById(id);
    },
  },
});
