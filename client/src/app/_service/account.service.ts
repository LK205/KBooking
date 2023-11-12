import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Account } from '../_model/account';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  readonly apiUrl = "https://localhost:44394/api/";
  constructor(private http: HttpClient) { } 

  login(email: string, password : string){
    let _url = this.apiUrl + "Account/Login?email=" + encodeURIComponent("" + email) + "&password=" + encodeURIComponent("" + password);
    return this.http.get<any>(_url); 
  }

  registerAccount(account: Account){
    let _url = this.apiUrl + "Account/Register";
    return this.http.post(_url, account); 
  }

  getHotel(id: number){
    let _url = this.apiUrl + "Account/GetHotel?id=" + encodeURIComponent("" + id);
    return this.http.get<any>(_url); 
  }
  
}
