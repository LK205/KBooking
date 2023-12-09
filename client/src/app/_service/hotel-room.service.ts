import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Room } from '../_model/room';

@Injectable({
  providedIn: 'root'
})
export class HotelRoomService {
  readonly apiUrl = "https://localhost:44394/api/";
  constructor(private http: HttpClient) { }

  GetAll(type: string, local: string, priceFrom: number, priceTo: number): Observable<any[]> {
    let _url = this.apiUrl + "HotelRoom/GetAllRoom?";

    if (type !== null || type !== undefined) {
      _url += "type=" + encodeURIComponent("" + type) + "&";
    }

    if (local !== null || local !== undefined) {
      _url += "local=" + encodeURIComponent("" + local) + "&";
    }

    if (priceFrom !== null || priceFrom !== undefined) {
      _url += "pricefrom=" + encodeURIComponent("" + priceFrom) + "&";
    }

    if (priceTo !== null || priceTo !== undefined) {
      _url += "priceTo=" + encodeURIComponent("" + priceTo) + "&";
    }
    _url = _url.replace(/[?&]$/, "");

    return this.http.get<any>(_url);
  }

  GetByHotelId(Id: number): Observable<any[]> {
    let _url = this.apiUrl + "HotelRoom/GetAllRoomByHotelID?HotelId=" + encodeURIComponent("" + Id);
    return this.http.get<any>(_url);
  }

  GetById(Id: number) {
    let _url = this.apiUrl + "HotelRoom/GetRoom?id=" + encodeURIComponent("" + Id);
    return this.http.get<any>(_url);
  }


  CreateOrUpdate(val: Room) {
    let _url = this.apiUrl + "HotelRoom/CreateOrUpdate";
    return this.http.post(_url, val);
  }

  Delete(Id: number) {
    let _url = this.apiUrl + "HotelRoom/Delete?id=" + encodeURIComponent("" + Id);
    return this.http.delete(_url);

  }
}
