import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, ReplaySubject } from 'rxjs';
import { Account } from '../_model/account';
import {map} from 'rxjs/operators';
@Injectable({
  providedIn: 'root'
})
export class AccountService {
  readonly apiUrl = "https://localhost:44394/api/";
  private currentUser = new ReplaySubject<any>(1);
  currentUser$ = this.currentUser.asObservable()
  constructor(private http: HttpClient) { } 

  login(email: string, password : string){
    let _url = this.apiUrl + "Account/Login?email=" + encodeURIComponent("" + email) + "&password=" + encodeURIComponent("" + password);
    return this.http.get<any>(_url)
    // .pipe(
    //   map((respone : any) =>{
    //     if(respone){
    //       localStorage.setItem('user', JSON.stringify(respone));
    //       this.currentUser.next(respone);
    //     }
    //   })
    // ); 
  }

  setCurrentUser(user: any){
    this.currentUser.next(user);
  }

  logOut(){
    localStorage.removeItem('user');
    this.currentUser.next(null);
  }

  registerAccount(account: Account){
    let _url = this.apiUrl + "Account/Register";
    return this.http.post(_url, account); 
  }

  getHotel(id: number){
    let _url = this.apiUrl + "Account/GetHotel?id=" + encodeURIComponent("" + id);
    return this.http.get<any>(_url); 
  }
  
  getById(id: number){
    let _url = this.apiUrl + "Account/GetById?id=" + encodeURIComponent("" + id);
    return this.http.get<any>(_url); 
  }

  updatePassword(id: number, newPassword: string, oldPassword: string){
    let _url = this.apiUrl + "Account/UpdatePassword?";
    if (id !== null || id !== undefined) {
      _url += "id=" + encodeURIComponent("" + id) + "&";
    }
    if (newPassword !== null || newPassword !== undefined) {
      _url += "newPassword=" + encodeURIComponent("" + newPassword) + "&";
    }
    if (oldPassword !== null || oldPassword !== undefined) {
      _url += "oldPassword=" + encodeURIComponent("" + oldPassword) + "&";
    }
    _url = _url.replace(/[?&]$/, "");
    return this.http.get<any>(_url); 
  }

  updateAccount(val: any){
    let _url = this.apiUrl + "Account/UpdateAccount";
    return this.http.put(_url, val);
  }

  deleteAccount(id: number){
    let _url = this.apiUrl + "Account/Delete?id=" + encodeURIComponent("" + id);
    return this.http.delete(_url);
  }
}
