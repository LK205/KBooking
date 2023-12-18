import { Component, OnInit } from '@angular/core';
import { RoomBillService } from '../_service/room-bill.service';
import { Bill } from '../_model/bill';

@Component({
  selector: 'app-room-tracking',
  templateUrl: './room-tracking.component.html',
  styleUrls: ['./room-tracking.component.css']
})
export class RoomTrackingComponent implements OnInit{
  user: any;
  pageSize: number = 10;
  pageNumber: number = 1;
  totalPage: number = 10;
  paginationTitle: string = "Page 1 of 1";
  request = "";
  status = "Chờ xác nhận";
  listData ?: any[];
  constructor(private _roomBillService: RoomBillService){}
  ngOnInit(): void {
    this.user = JSON.parse(localStorage.getItem('user') || "0")
    this.getData();
  }

  getData(){
    if(this.user.role === 1){
      this.getAllBillHotel();
    }else{
      this.getAllBillCus();
    }
    
    
  }

  getAllBillCus(){
    this._roomBillService.getRoomBillByCusId(this.user.id,this.status,this.request,this.pageSize,this.pageNumber).subscribe(res =>{
      this.listData = res.roomBill;
      this.listData.forEach(e=>{
        e.fromBokDate = new Date(e.fromBokDate).toLocaleDateString();
        e.toBokDate = new Date(e.toBokDate).toLocaleDateString();
      })
      this.totalPage = Math.ceil(res.total[0].totalCount / this.pageSize);
      this.paginationTitle = "Page " + this.pageNumber +" of " + (this.totalPage == 0 ? 1 : this.totalPage) ;
    })
  }

  getAllBillHotel(){
    this._roomBillService.getRoomBillByHotelId(this.user.id,this.status,this.request,this.pageSize,this.pageNumber).subscribe(res =>{
      this.listData = res.roomBill;
      this.listData.forEach(e=>{
        e.fromBokDate = new Date(e.fromBokDate).toLocaleDateString();
        e.toBokDate = new Date(e.toBokDate).toLocaleDateString();
      })
      this.totalPage = Math.ceil(res.total[0].totalCount / this.pageSize);
      this.paginationTitle = "Page " + this.pageNumber +" of " + (this.totalPage == 0 ? 1 : this.totalPage) ;
    })
  }

  search(event: any){
    if(event.keyCode === 13){
      this.pageNumber == 1
      if(this.user.role === 1){
        this.getAllBillHotel();
      }else{
        this.getAllBillCus();
      }
    }
  }

  confirmBill(item: any){
    let  bill  = {
      id : item.id,
      customerId : item.customerId,
      name: item.name,
      phoneNumber: item.phoneNumber,
      hotelId : item.hotelId,
      roomId : item.roomId,
      status : "Thành công",
      priceTotal : item.priceTotal,
      additionalServices : item.additionalServices,
      fromBokDate : Date,
      toBokDate : Date,
      craetionTime : Date,
    }
    this._roomBillService.confirmRoomBill(bill).subscribe(res => {
      this.getData();
    });
  }

  cancelBill(item: any){
    if(confirm("Bạn có chắc chắn HỦY đơn đặt phòng?")){
      let  bill  = {
        id : item.id,
        customerId : item.customerId,
        name: item.name,
        phoneNumber: item.phoneNumber,
        hotelId : item.hotelId,
        roomId : item.roomId,
        status : "Hủy",
        priceTotal : item.priceTotal,
        additionalServices : item.additionalServices,
        fromBokDate : Date,
        toBokDate : Date,
        craetionTime : Date,
      }
      this._roomBillService.confirmRoomBill(bill).subscribe(res => {
        this.getData();
      });
    }
  }
  previousPage() {
    if (this.pageNumber > 1) {
      this.pageNumber -= 1;
      this.getData();
    }
  }
  nextPage() {
    if (this.pageNumber < this.totalPage) {
      this.pageNumber += 1;
      this.getData();
    }
  }
}
