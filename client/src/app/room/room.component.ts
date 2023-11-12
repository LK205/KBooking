import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-room',
  templateUrl: './room.component.html',
  styleUrls: ['./room.component.css']
})
export class RoomComponent implements OnInit{
  hotelId: any = 0;
  data: any;

  constructor(private router: ActivatedRoute){}

  ngOnInit(): void {
    this.hotelId =  this.router.snapshot.paramMap.get('id');
    console.log(this.hotelId);
  }



  onselectionFile(file: any){
    var reader = new FileReader();
    reader.readAsDataURL(file.target.files[0]);
    reader.onloadend = (value: any) => {
      this.data = value.target.result.toString();
    }
  }
}
