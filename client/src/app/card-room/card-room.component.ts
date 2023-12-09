import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-card-room',
  templateUrl: './card-room.component.html',
  styleUrls: ['./card-room.component.css']
})
export class CardRoomComponent  implements OnInit{
  @Input() dataHotel : any;

  constructor(private router: Router){}
  ngOnInit(): void {
  }



  click(){
    this.router.navigateByUrl('YourHotel/' +this.dataHotel.id);
  }
}
