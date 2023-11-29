import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HotelRoomService } from '../_service/hotel-room.service';
import { Room } from '../_model/room';

@Component({
  selector: 'app-room',
  templateUrl: './room.component.html',
  styleUrls: ['./room.component.css']
})
export class RoomComponent implements OnInit{
  hotelId: any = 0;
  dataRoom: Room = {
    id : 0,
    hotelId : this.hotelId,
    roomName : "",
    roomType : "",
    bedType : "",
    roomImage : "",
    price  : 0,
    roomArea : "",
    decription : "",
  };

  constructor(private router: ActivatedRoute, private _service: HotelRoomService){}

  ngOnInit(): void {
    this.hotelId =  this.router.snapshot.paramMap.get('id');
    console.log(this.hotelId);
  }

  onselectionFile(file: any){
    var reader = new FileReader();
    reader.readAsDataURL(file.target.files[0]);
    reader.onloadend = (value: any) => {
      this.dataRoom.roomImage = value.target.result.toString();
    }
  }

  createRoom(){
    this.dataRoom.hotelId = this.hotelId;
    if(!this.isCheck()){
      alert("Nhập đủ các trường!");
      return;
    }
    this._service.CreateRoom(this.dataRoom).subscribe(res=>{
      alert("Thêm phòng thành công!");
      this.reset();
    },
    error =>{
      alert("Đã xảy ra lỗi gì đó! Hãy thao tác lại!");
    })
  }

  reset(){
    this.dataRoom = {
      id : 0,
      hotelId : this.hotelId,
      roomName : "",
      roomType : "",
      bedType : "",
      roomImage : "",
      price  : 0,
      roomArea : "",
      decription : "",
    }
  }


  isCheck(){
    if(this.dataRoom.roomName.length <= 0 ||
      this.dataRoom.roomImage.length <= 0 ||
      this.dataRoom.roomType.length <= 0 ||
      this.dataRoom.roomArea.length <= 0 ||
      this.dataRoom.price <= 0 ) return false;
    
    
    return true;
  }
}
