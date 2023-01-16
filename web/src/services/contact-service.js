class ContactService {
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
      .then(this._checkStatus)
      .then((response) => response.json());
  };

  _checkStatus(response) {
    console.log(`ContactService -> checkStatus`, response);
    if (response.status >= 200 && response.status < 300) {
      return response;
    } else {
      var error = new Error(response.statusText);
      error.response = response;
      throw error;
    }
  }

  findAll() {
    return this._fetch(`${this._baseUrl}/Contact`);
  }
}

export default {
  install(app) {
    app.config.globalProperties.$contactService = new ContactService();
  }
}