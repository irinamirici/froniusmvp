import { Injectable, Inject } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { PhotovoltaicSystem, PhotovoltaicSystemDetail } from "../models/photovoltaic-system";
import { Observable } from "rxjs";
import { Paged } from "../models/paged";

@Injectable()
export class PhotovoltaicSystemService {

  baseUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  findPhotovoltaicSystems(sortBy = '', sortOrder = 'asc', pageNumber = 0, pageSize = 5): any {
    return this.http.get<Paged<PhotovoltaicSystem>>(this.baseUrl + 'photovoltaicsystems', {
      params: new HttpParams()
        .set('sortBy', sortBy)
        .set('sortOrder', sortOrder)
        .set('pageNumber', pageNumber.toString())
        .set('pageSize', pageSize.toString())
    });
  }

  findSystemById(id: string): Observable<PhotovoltaicSystemDetail> {
    return this.http.get<PhotovoltaicSystemDetail>(this.baseUrl + 'photovoltaicsystems/' + id);
  }
}
