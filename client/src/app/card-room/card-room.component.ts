import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-card-room',
  templateUrl: './card-room.component.html',
  styleUrls: ['./card-room.component.css']
})
export class CardRoomComponent {
  constructor(private router: Router){}



  click(){
    this.router.navigateByUrl('YourHotel/' +2)
  }
}
