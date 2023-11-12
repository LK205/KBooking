import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { RegisterUserComponent } from './register-user/register-user.component';
import { RegisterHotelComponent } from './register-hotel/register-hotel.component';
import { HotelRoomsComponent } from './hotel-rooms/hotel-rooms.component';
import { YourHotelComponent } from './your-hotel/your-hotel.component';
import { RoomComponent } from './room/room.component';

const routes: Routes = [
  {path: "", component: HomeComponent},
  {path: "RegisterUser", component: RegisterUserComponent},
  {path: "RegisterHotel", component: RegisterHotelComponent},
  {path: "HotelRooms", component: HotelRoomsComponent},
  {path: "YourHotel/:id", component: YourHotelComponent},
  {path: "Room/:id", component: RoomComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
