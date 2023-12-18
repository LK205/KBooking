import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NavigationComponent } from './navigation/navigation.component';
import { FooterComponent } from './footer/footer.component';
import { HomeComponent } from './home/home.component';
import { RegisterUserComponent } from './register-user/register-user.component';
import { RegisterHotelComponent } from './register-hotel/register-hotel.component';
import { HotelRoomsComponent } from './hotel-rooms/hotel-rooms.component';
import { YourHotelComponent } from './your-hotel/your-hotel.component';
import { RoomComponent } from './room/room.component';
import { CardRoomComponent } from './card-room/card-room.component';
import { RoomInHotelComponent } from './room/room-in-hotel/room-in-hotel.component';
import { RoomTrackingComponent } from './room-tracking/room-tracking.component';
import { InformationAccountComponent } from './information-account/information-account.component';
@NgModule({
  declarations: [
    AppComponent,
    NavigationComponent,
    FooterComponent,
    HomeComponent,
    RegisterUserComponent,
    RegisterHotelComponent,
    HotelRoomsComponent,
    CardRoomComponent,
    YourHotelComponent,
    RoomComponent,
    RoomInHotelComponent,
    RoomTrackingComponent,
    InformationAccountComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
