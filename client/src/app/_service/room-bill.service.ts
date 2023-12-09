import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RoomBillService {
  readonly apiUrl = "https://localhost:44394/api/";
  constructor(private http: HttpClient) { }

  getRoomBillByHotelId(id: number, status: string, request: string, pageSize: number, pageNumber: number): Observable<{roomBill: any[], total: any[]}>{
    let url = this.apiUrl + "RoomBill/GetAllBillByHotelId?";
    if(id !== undefined && id !== null){
      url += "HotelId=" + encodeURIComponent("" + id) + "&";
    }
    if(status !== undefined && status !== null){
      url += "Status=" + encodeURIComponent("" + status) + "&";
    }
    if(request !== undefined && request !== null){
      url += "request=" + encodeURIComponent("" + request) + "&";
    }
    if(pageSize !== undefined && pageSize !== null){
      url += "pageSize=" + encodeURIComponent("" + pageSize) + "&";
    }
    if(pageNumber !== undefined && pageNumber !== null){
      url += "pageNumber=" + encodeURIComponent("" + pageNumber) + "&";
    }
    return this.http.get<any>(url);
  }

  getRoomBillByCusId(id: number, status: string,request: string, pageSize: number, pageNumber: number): Observable<{roomBill: any[], total: any[]}>{
    let url = this.apiUrl + "RoomBill/GetAllBillByCusId?";
    if(id !== undefined && id !== null){
      url += "CuslId=" + encodeURIComponent("" + id) + "&";
    }
    if(status !== undefined && status !== null){
      url += "Status=" + encodeURIComponent("" + status) + "&";
    }
    if(request !== undefined && request !== null){
      url += "request=" + encodeURIComponent("" + request) + "&";
    }
    if(pageSize !== undefined && pageSize !== null){
      url += "pageSize=" + encodeURIComponent("" + pageSize) + "&";
    }
    if(pageNumber !== undefined && pageNumber !== null){
      url += "pageNumber=" + encodeURIComponent("" + pageNumber) + "&";
    }
    return this.http.get<any>(url);
  }

  createRoomBill(val: any){
    let url = this.apiUrl + "RoomBill/Create";
    return this.http.post(url, val);
  }

  confirmRoomBill(val: any){
    let url = this.apiUrl + "RoomBill/Confirm";
    return this.http.put(url, val);
  }
}
