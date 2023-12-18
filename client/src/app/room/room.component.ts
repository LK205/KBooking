import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HotelRoomService } from '../_service/hotel-room.service';
import { Room } from '../_model/room';
declare var $:any;
@Component({
  selector: 'app-room',
  templateUrl: './room.component.html',
  styleUrls: ['./room.component.css']
})
export class RoomComponent implements OnInit {
  hotelId: any = 0;
  listData: any[] | undefined;
  roomId:  number = 0;
  dataRoom: Room = {
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
  constructor(private router: ActivatedRoute, private _service: HotelRoomService) { }

  ngOnInit(): void {
    this.hotelId = this.router.snapshot.paramMap.get('id');
    this.getAll();
  }

  onselectionFile(file: any) {
    var reader = new FileReader();
    reader.readAsDataURL(file.target.files[0]);
    reader.onloadend = (value: any) => {
      this.dataRoom.roomImage = value.target.result.toString();
    }
  }

  getAll() {
    this._service.GetByHotelId(this.hotelId).subscribe(res => {
      this.listData = res;
    })
  }

  createOrUpdateRoom() {
    this.dataRoom.hotelId = this.hotelId;
    if (this.dataRoom.roomName.trim() === "" || this.dataRoom.roomName.length <= 0) {
      alert("Trường Tên phòng không được để trống!");
      return;
    }
    if (this.dataRoom.bedType.trim() === "" || this.dataRoom.bedType.length <= 0) {
      alert("Trường Loại giường không được để trống!");
      return;
    }
    if (this.dataRoom.roomType.trim() === "" || this.dataRoom.roomType.length <= 0) {
      alert("Trường Loại phòng không được để trống!");
      return;
    }
    if (this.dataRoom.price === null || this.dataRoom.price === 0) {
      alert("Trường Giá phòng không được để trống!");
      return;
    }
    if (this.dataRoom.roomArea.trim() === "" || this.dataRoom.roomArea.length <= 0) {
      alert("Trường Diện tích phòng không được để trống!");
      return;
    }
    if (this.dataRoom.roomImage.trim() === "" || this.dataRoom.roomImage.length <= 0) {
      alert("Hãy chọn hình ảnh cho phòng!");
      return;
    }

    this._service.CreateOrUpdate(this.dataRoom).subscribe(res => {
      this.reset();
      this.getAll();
      this.closeModal();
    },
      error => {
        alert("Đã xảy ra lỗi gì đó! Hãy thao tác lại!");
      })
  }

  deleteRoom(id: number) {
    if (confirm("Bạn có chắc chắc muốn thực hiện thao tác này không?")) {
      this._service.Delete(id).subscribe(res => {
        alert("Xóa phòng thành công!");
        this.getAll();
      })
    }
  }

  showModal(){
    if(this.dataRoom.id == 0){
      $('#staticBackdropLabel').text("Thêm phòng");
    }else $('#staticBackdropLabel').text("Sửa phòng")
    $('#staticBackdrop').modal('show');
  }
  closeModal(){
    this.roomId == 0;
    $('#staticBackdrop').modal('hide');
    this.reset();
  }

  getByRoomId(roomId:number){
    this._service.GetById(roomId).subscribe(res => {
      this.dataRoom = {
        id: res.id,
        hotelId: res.hotelId,
        roomName: res.roomName,
        roomType: res.roomType,
        bedType: res.bedType,
        roomImage: res.roomImage,
        price: res.price,
        roomArea: res.roomArea,
        decription: "",
      }
      this.showModal();
    },
    error => {
      alert("Something Wrong =((")
    });
    
  }


  reset() {
    this.dataRoom = {
      id: 0,
      hotelId: this.hotelId,
      roomName: "",
      roomType: "",
      bedType: "",
      roomImage: "",
      price: 0,
      roomArea: "",
      decription: "",
    }
  }

  
}
