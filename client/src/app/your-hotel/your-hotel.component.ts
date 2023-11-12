import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Account } from '../_model/account';
import { AccountService } from '../_service/account.service';

@Component({
  selector: 'app-your-hotel',
  templateUrl: './your-hotel.component.html',
  styleUrls: ['./your-hotel.component.css']
})
export class YourHotelComponent implements OnInit {
  hotelId: any = 0;
  hotelData : Account ={
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
  constructor(private router: ActivatedRoute, private _service : AccountService){}
  ngOnInit(): void {
    this.hotelId =  this.router.snapshot.paramMap.get('id');
    this._service.getHotel(this.hotelId).subscribe(res=>{
      this.hotelData = res;
    })
  }

}
