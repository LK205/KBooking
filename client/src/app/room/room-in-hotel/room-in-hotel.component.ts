import { Component, Input, OnInit } from '@angular/core';
import { HotelRoomService } from 'src/app/_service/hotel-room.service';

@Component({
  selector: 'app-room-in-hotel',
  templateUrl: './room-in-hotel.component.html',
  styleUrls: ['./room-in-hotel.component.css']
})
export class RoomInHotelComponent implements OnInit {
  @Input() dataRoom : any;
  
  constructor (private _service: HotelRoomService){}
  ngOnInit(): void {
  }

  
}
