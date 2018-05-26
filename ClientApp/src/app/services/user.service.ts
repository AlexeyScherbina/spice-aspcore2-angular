import { Injectable } from '@angular/core';

@Injectable()

export class UserService {

  public getToken() {
    return sessionStorage.getItem("token");
  }
  
  public setToken(token: string) {
    sessionStorage.setItem("token", token);
  }

  public unsetUser() {
    sessionStorage.removeItem("token");
  }
}
