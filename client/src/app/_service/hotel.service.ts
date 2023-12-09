import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HotelService {
  readonly apiUrl = "https://localhost:44394/api/";
  constructor(private http: HttpClient) { }

  getAllHotel(request: string, pageSize: number, pageNumber: number): Observable<{hotels: any[], total: any[] }> {
    let _url = this.apiUrl + "Account/GetAllHotel?";

    if (request !== null || request !== undefined) {
      _url += "request=" + encodeURIComponent("" + request) + "&";
    }

    if (pageSize !== null || pageSize !== undefined) {
      _url += "pageSize=" + encodeURIComponent("" + pageSize) + "&";
    }

    if (pageNumber !== null || pageNumber !== undefined) {
      _url += "pageNumber=" + encodeURIComponent("" + pageNumber) + "&";
    }
    _url = _url.replace(/[?&]$/, "");

    return this.http.get<any>(_url);
  }

}
