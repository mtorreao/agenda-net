import { useAuthStore } from "../stores/auth";

class ContactService {
  // _baseUrl = "http://agenda-net-api:5000/api/v1";
  _baseUrl = "http://localhost:5000/api/v1";
  _fetch = (url, options) => {
    const headers = {
      Accept: "application/json",
      "Content-Type": "application/json",
      Authorization: "Bearer " + this._getToken(),
    };

    return fetch(url, {
      headers,
      ...options,
    })
      .then((response) => {
        if (response.status >= 200 && response.status < 300) {
          return response;
        } else if (response.status === 401) {
          useAuthStore().logout();
          return;
        } else {
          var error = new Error(response.statusText);
          error.response = response;
          throw error;
        }
      })
      .then((response) => response.json());
  };

  _getToken() {
    return localStorage.getItem("token");
  }

  findAll() {
    return this._fetch(`${this._baseUrl}/Contact`, {
      method: "GET",
    });
  }

  create({ name, email, phone }) {
    return this._fetch(`${this._baseUrl}/Contact`, {
      method: "POST",
      body: JSON.stringify({ name, email, phone }),
    });
  }

  update({ id, name, email, phone }) {
    return this._fetch(`${this._baseUrl}/Contact`, {
      method: "PATCH",
      body: JSON.stringify({ id, name, email, phone }),
    });
  }

  delete({ id }) {
    return this._fetch(`${this._baseUrl}/Contact`, {
      method: "DELETE",
      body: JSON.stringify({ id }),
    });
  }

  getById(id) {
    return this._fetch(`${this._baseUrl}/Contact/${id}`, {
      method: "GET",
    });
  }
}

export default {
  instance: new ContactService(),
};
