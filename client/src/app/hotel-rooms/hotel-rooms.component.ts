import { Component, OnInit } from '@angular/core';
import { HotelRoomService } from '../_service/hotel-room.service';
import { HotelService } from '../_service/hotel.service';
declare var $: any;
@Component({
  selector: 'app-hotel-rooms',
  templateUrl: './hotel-rooms.component.html',
  styleUrls: ['./hotel-rooms.component.css']
})
export class HotelRoomsComponent implements OnInit {
  request: string = "";
  pageSize: number = 10;
  pageNumber: number = 1;
  totalPage: number = 10;
  paginationTitle: string = "";
  listHotelData: any[] | undefined;
  constructor(private _hotelService: HotelService) { }
  ngOnInit(): void {
    this.getAllRoom()
  }

  getAllRoom() {
    this._hotelService.getAllHotel(this.request, this.pageSize, this.pageNumber).subscribe((res: { hotels: any[], total: any[] }) => {
      this.listHotelData = res.hotels;
      this.totalPage = Math.ceil(res.total[0].totalCount / this.pageSize);
      this.paginationTitle = "Page " + this.pageNumber + " of " + this.totalPage;
    })
  }

  search(event?: any){
    if(event.keyCode === 13){
      this.pageNumber = 1;
      this.getAllRoom();
    }
  }

  previousPage() {
    if (this.pageNumber > 1) {
      this.pageNumber -= 1;
      this.getAllRoom();
    }
  }
  nextPage() {
    if (this.pageNumber < this.totalPage) {
      this.pageNumber += 1;
      this.getAllRoom();
    }
  }


}
