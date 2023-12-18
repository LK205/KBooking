import { Component, OnInit } from '@angular/core';
import { Account } from '../_model/account';
import { AccountService } from '../_service/account.service';

@Component({
  selector: 'app-register-hotel',
  templateUrl: './register-hotel.component.html',
  styleUrls: ['./register-hotel.component.css']
})
export class RegisterHotelComponent implements OnInit {
  dataAccount: Account = {
    id: 0,
    email: "",
    phoneNumber: "",
    password: "",
    creationTime: new Date,
    role: 1,

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
  confirmPass: string = "";
  regEmail = /^\w+@[a-zA-Z]+\.com$/i;
  checkBox: boolean = false;

  constructor(private _service: AccountService) { }
  ngOnInit(): void {

  }

  register() {
    if (this.isValid()) {
      if (!this.regEmail.test(this.dataAccount.email)) {
        alert("Email không hợp lệ!!!");
      }
      else if (!this.checkBox) {
        alert("Đồng ý điều khoản của chúng tôi!")
      }
      else if (this.confirmPass !== this.dataAccount.password) {
        alert("Mật xác thực chưa chính xác!")
      }
      else {
        this.dataAccount.password.trim();
        this.dataAccount.email.trim();
        this.dataAccount.hotelName.trim();
        this.dataAccount.address_City.trim();
        this.dataAccount.address_District.trim();
        this.dataAccount.generalDescription.trim();
        this.dataAccount.locationDescription.trim();

        this._service.registerAccount(this.dataAccount).subscribe(res => {
          this.reset();
          alert("Đăng ký thành công!");
        },
          error => {
            alert("Email đã được sử dụng!")
          });
      }
    }
    else {
      alert("Nhập đủ các ô dữ liệu!")
    }

  }


  isValid() {
    if (this.dataAccount.hotelName.length <= 0 || this.dataAccount.phoneNumber.length <= 0 ||
      this.dataAccount.address_City.length <= 0 || this.dataAccount.address_District.length <= 0 ||
      this.dataAccount.address_Specifically.length <= 0 || this.dataAccount.email.length <= 0 ||
      this.dataAccount.password.length <= 0 || this.confirmPass.length <= 0) return false;
    return true;
  }



  reset() {
    this.dataAccount = {
      id: 0,
      email: "",
      phoneNumber: "",
      password: "",
      creationTime: new Date,
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
    this.confirmPass = "";
  }
}
