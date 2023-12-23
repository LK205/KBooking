import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Account } from '../_model/account';
import { AccountService } from '../_service/account.service';
import { HotelRoomService } from '../_service/hotel-room.service';
import { Room } from '../_model/room';
import { Bill } from '../_model/bill';
import { RoomBillService } from '../_service/room-bill.service';
declare var $: any;
@Component({
  selector: 'app-your-hotel',
  templateUrl: './your-hotel.component.html',
  styleUrls: ['./your-hotel.component.css']
})
export class YourHotelComponent implements OnInit {
  hotelId: any = 0;
  hotelData: Account = {
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
  CustomerId: number = 0;
  listRoomData: any[] | undefined;
  minPrice = 0;
  currentDate = (new Date());
  roomData: Room = {
    id: 0,
    hotelId: 0,
    roomName: "",
    roomType: "",
    bedType: "",
    roomImage: "",
    price: 0,
    roomArea: "",
    decription: "",
  };

  roomBill: Bill = {
    id: 0,
    customerId: this.CustomerId,
    name: "",
    phoneNumber: "",
    hotelId: this.hotelId,
    roomId: 0,
    status: "",
    priceTotal: 0,
    additionalServices: "",
    fromBokDate: new Date(),
    toBokDate: new Date(),
    craetionTime: new Date(),
  }
  totalNight: number = 0;

  constructor(private router: ActivatedRoute, private _service: AccountService, private _hotelService: HotelRoomService,
    private _billService: RoomBillService) { }

  ngOnInit(): void {
    this.hotelId = this.router.snapshot.paramMap.get('id');
    this._service.getHotel(this.hotelId).subscribe(res => {
      this.hotelData = res;
    })
    this.getAllRoom();
    this.CustomerId = JSON.parse(localStorage.getItem('user') || "0").id;
    console.log(this.hotelData);
    
  }


  getAllRoom() {
    this._hotelService.GetByHotelId(this.hotelId).subscribe(res => {
      this.listRoomData = res;
      let price = 100000000;
      res.forEach(e => {
        if (e.price <= price) {
          price = e.price;
        }
      })
      this.minPrice = price;
    })
  }

  getRoomById(id: number) {
    this._hotelService.GetById(id).subscribe(res => {
      this.roomData = res;
      this.roomBill.roomId = res.id;
      $('#roomBillModal').modal("show");
    },
      error => {
        alert(error);
      }
    )
  }

  changeDate(event: any) {
    let day = Math.ceil((new Date(this.roomBill.toBokDate).getTime() - new Date(this.roomBill.fromBokDate).getTime()) / (1000 * 60 * 60 * 24));
    this.totalNight = day > 0 ? day : 0;
  }

  createBill() {
    this.roomBill.hotelId = parseInt(this.hotelId);
    this.roomBill.customerId = this.CustomerId;
    this.roomBill.priceTotal = this.totalNight * this.roomData.price;

    this.currentDate.setDate(new Date().getDate() - 1).toString();
    if (this.totalNight === 0 || new Date(this.roomBill.fromBokDate) < new Date(this.currentDate)) {
      alert("Ngày chưa hợp lệ!");
      return;
    }
    if(this.roomBill.name === null ||this.roomBill.name.trim() ==="" ){
      alert("Trường Tên không được để trống!");
      return;
    }
    if(this.roomBill.phoneNumber === null ||this.roomBill.phoneNumber.trim() ==="" ){
      alert("Trường Số điện thoại không được để trống!");
      return;
    }
    this._billService.createRoomBill(this.roomBill).subscribe(res =>{
      $('#roomBillModal').modal('hide');
      alert("Đặt phòng thành công!");
      this.reset();
    })
  }

  reset() {
    this.roomBill = {
      id: 0,
      customerId: 0,
      name: "",
      phoneNumber: "",
      hotelId: 0,
      roomId: 0,
      status: "",
      priceTotal: 0,
      additionalServices: "",
      fromBokDate: new Date(),
      toBokDate: new Date(),
      craetionTime: new Date(),
    }
  }
}
