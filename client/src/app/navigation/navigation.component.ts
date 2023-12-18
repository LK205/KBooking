import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { AccountService } from '../_service/account.service';
import { Account } from '../_model/account';
import { Router } from '@angular/router';
declare var $: any;
@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit {

  email: string = "";
  passWord: string = "";
  dataAccount: Account = {
    id: 0,
    email: "",
    phoneNumber: "",
    password: "",
    creationTime:  new Date,
    role: 0,

    middleName: "",
    lastName: "",
    gender: 0,
    dayOfBirth: new Date,
    cityOfResidence: "",
    imageBase64: "",
    isActive: true,

    hotelName: "",
    address_City: "",
    address_District: "",
    address_Ward: "",
    address_Specifically: "",
    avatar: "",
    website: "",
    locationDescription: "",
    generalDescription: "",
  };
  isLogin: boolean = false;
  constructor(private _serviceAccount: AccountService,
    private router: Router) { }

  ngOnInit(): void {
    this.getCurrentUser();
  }

  login() {
    this._serviceAccount.login(this.email, this.passWord).subscribe(res => {
      this.dataAccount = res;
      this.dataAccount.avatar =  (this.dataAccount.avatar === null ||  this.dataAccount.avatar.trim() === "") ? null :   res.avatar;
        this.isLogin = true;
        if(this.dataAccount.role === 0 ){
          this.router.navigateByUrl('HotelRooms');
        }
        if(this.dataAccount.role === 1 ){
          this.router.navigateByUrl('YourHotel/' + res.id);
        }

        localStorage.setItem('user', JSON.stringify(res));
        this._serviceAccount.setCurrentUser(res);
        $('#close_modal').click();
    },
      error => {
        alert("Email hoặc mật khẩu không chính xác!");
      })
  }

  logout() {
    this._serviceAccount.logOut();
    this.isLogin = false;
  }

  getCurrentUser(){
    this._serviceAccount.currentUser$.subscribe(user =>{
      this.isLogin = !!user;
      this.dataAccount = user ?? {
        id: 0,
        email: "",
        phoneNumber: "",
        password: "",
        creationTime:  new Date,
        role: 0,
    
        middleName: "",
        lastName: "",
        gender: 0,
        dayOfBirth: new Date,
        cityOfResidence: "",
        imageBase64: "",
        isActive: true,
    
        hotelName: "",
        address_City: "",
        address_District: "",
        address_Ward: "",
        address_Specifically: "",
        avatar: "",
        website: "",
        locationDescription: "",
        generalDescription: "",
      };
    })
  }

  KtraKhachSan() {
    if (!this.isLogin) {
      alert("Đăng nhập để sử dụng các tính năng của trang web!");
    }
    else {
      this.router.navigateByUrl('HotelRooms');
    }
  }

}
