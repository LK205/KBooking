import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_service/account.service';

@Component({
  selector: 'app-information-account',
  templateUrl: './information-account.component.html',
  styleUrls: ['./information-account.component.css']
})
export class InformationAccountComponent implements OnInit {

  userData: any;
  currentPassword = "";
  confirmPassword = "";
  dateFormat = "";
  lastName = "";
  middleName = "";
  phoneNumber = "";
  avatar = "";
  hotelName = "";
  website ="";
  email="";
  district = "";
  province = "";
  address = "";
  description= "";
  constructor(private _service: AccountService){}
  ngOnInit(): void {
    this.setup();

  }

  setup(){
    this.userData = JSON.parse(localStorage.getItem('user') || "0");
    this.userData.avatar = (this.userData.avatar === null || this.userData.avatar.trim() === "") ? null : this.userData.avatar;
    this.userData.dayOfBirth = new Date(this.userData.dayOfBirth).toLocaleDateString()
    this.userData.passWord = "";

    this.lastName = this.userData.lastName;
    this.middleName = this.userData.middleName;
    this.phoneNumber = this.userData.phoneNumber;
    this.avatar = (this.userData.avatar === null || this.userData.avatar.trim() === "") ? null : this.userData.avatar;
    this.phoneNumber = this.userData.phoneNumber;
    this.hotelName = this.userData.hotelName;
    this.website =this.userData.website;
    this.email=this.userData.email;
    this.district = this.userData.address_District;
    this.province = this.userData.address_City;
    this.address = this.userData.address_Specifically;
    this.description= this.userData.generalDescription;

    let parts = this.userData.dayOfBirth.split('/');
    let date = new Date(`${parts[1]}/${parts[0]}/${parts[2]}`);
    this.dateFormat = this.getFormattedDate(date);
  }


  updateAccount(){
    if(this.userData.role === 0){
      if(this.middleName.trim() === "") return alert("Họ không được để trống!");
      if(this.lastName.trim() === "") return alert("Tên không được để trống!");
      if(this.dateFormat === null || this.dateFormat.trim() === "") return alert("Ngày không hợp lệ!");
    }
    else{
      if(this.hotelName.trim() === "") return alert("Tên khách sạn không được để trống!");
      if(this.website.trim() === "") return alert("Website không được để trống!");
      if(this.district.trim() === "") return alert("Huyện/Xã không được để trống!");
      if(this.province.trim() === "") return alert("Tỉnh/Thành phố không được để trống!");
      if(this.address.trim() === "") return alert("Địa chỉ chi tiết không được để trống!");
    }

    if(this.phoneNumber.trim() === "") return alert("Điện thoại không được để trống!");

    
    this.userData.lastName = this.lastName.trim();
    this.userData.middleName = this.middleName.trim();
    this.userData.phoneNumber = this.phoneNumber.trim();
    this.userData.avatar = this.avatar ?? "";
    this.userData.phoneNumber = this.phoneNumber.trim();
    this.userData.hotelName = this.hotelName.trim();
    this.userData.website = this.website.trim();
    this.userData.address_District = this.district.trim();
    this.userData.address_City = this.province.trim();
    this.userData.address_Specifically = this.address.trim();
    this.userData.generalDescription = this.description.trim();
    
    this.userData.dayOfBirth = new Date(this.dateFormat);
    this._service.updateAccount(this.userData).subscribe(res=>{
      localStorage.setItem('user', JSON.stringify(this.userData));
      this._service.setCurrentUser(this.userData);
      this.userData.dayOfBirth = new Date(this.userData.dayOfBirth).toLocaleDateString()
      alert("Cập nhật thành công!");
    })
  }

  changePassword(){
    if(this.currentPassword.trim() === "") return alert("Mật khẩu hiện tại KHÔNG được để trống!")
    if(this.userData.passWord.trim() === "") return alert("Mật khẩu mới KHÔNG được để trống!")
    if(this.confirmPassword.trim() === "") return alert("Xác nhận Mật khẩu KHÔNG được để trống!")
    if(this.userData.passWord.trim() !== this.confirmPassword.trim()) return alert("Mật khẩu xác nhận KHÔNG khớp!")


    this._service.updatePassword(this.userData.id,this.userData.passWord , this.currentPassword).subscribe(res=>{
      if(res){
        this.userData.passWord = "";
        this.currentPassword = "";
        this.confirmPassword = "";
        alert("Đổi mật khẩu thành công!");
      }
      else{
        alert("Mật khẩu hiện tại KHÔNG đúng!");
      }
    },
    error =>{
      alert("Đã xảy ra lỗi! Hãy kiểm tra lại đường truyền!");
    })
  }
  onselectionFile(file: any) {
    var reader = new FileReader();
    reader.readAsDataURL(file.target.files[0]);
    reader.onloadend = (value: any) => {
      this.avatar = value.target.result.toString();
    }
  }
  getFormattedDate(date: Date): string {

    const day = date.getDate().toString().padStart(2, '0');
    const month = (date.getMonth() + 1).toString().padStart(2, '0');
    const year = date.getFullYear().toString();
    return `${year}-${month}-${day}`;
  }
}