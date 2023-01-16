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

  login(email, password) {
    return this._fetch(`${this._baseUrl}/auth/signin`, {
      method: "POST",
      body: JSON.stringify({
        email: email,
        password: password,
      }),
    }).then((response) => {
      if (response.success === true && response.data.accessToken) {
        this._setToken(response.data.accessToken);
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
      if (response.success === true && response.data.token.accessToken) {
        this._setToken(response.data.token.accessToken);
      }
      return response;
    });
  }

  logout() {
    this._clearToken();
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
  instance: new AuthService(),
};
