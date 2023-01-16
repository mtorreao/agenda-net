class AuthService {
  _baseUrl = "http://localhost:5000/api/v1";
  _fetch = (url, options) => {
    const headers = {
      Accept: "application/json",
      "Content-Type": "application/json",
    };

    return fetch(url, {
      headers,
      ...options,
    }).then((response) => response.json());
  };

  constructor() {
    this.authenticated = false;
  }

  login(email, password) {
    return this._fetch(`${this._baseUrl}/auth/signin`, {
      method: "POST",
      body: JSON.stringify({
        email: email,
        password: password,
      }),
    }).then((response) => {
      if (response.success === true) {
        this._setToken(response.data.accessToken);
        this.authenticated = true;
      }
      return response;
    });
  }

  register({ email, password, name }) {
    return this._fetch(`${this._baseUrl}/auth/signup`, {
      method: "POST",
      body: JSON.stringify({
        email: email,
        password: password,
        name: name,
      }),
    }).then((response) => {
      if (response.success === true) {
        this._setToken(response.data.accessToken);
        this.authenticated = true;
      }
      return response;
    });
  }

  logout() {
    this.authenticated = false;
    this._clearToken();
  }

  isAuthenticated() {
    if (this._getToken()) this.authenticated = true;
    else this.authenticated = false;
    return this.authenticated;
  }

  _setToken(token) {
    localStorage.setItem("token", token);
  }

  _getToken() {
    return localStorage.getItem("token");
  }

  _clearToken() {
    localStorage.removeItem("token");
  }
}

export default {
  instance: () => new AuthService(),
};
