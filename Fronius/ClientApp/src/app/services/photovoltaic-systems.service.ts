import { Injectable, Inject } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { map } from "rxjs/operators";

@Injectable()
export class PhotovoltaicSystemService {

  baseUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  findPhotovoltaicSystems(sortBy = '', sortOrder = 'asc', pageNumber = 0, pageSize = 5): any {

    return this.http.get(this.baseUrl + 'photovoltaicsystems', {
      params: new HttpParams()
        .set('sortBy', sortBy)
        .set('sortOrder', sortOrder)
        .set('pageNumber', pageNumber.toString())
        .set('pageSize', pageSize.toString())
    }).pipe(
      map((res: any) => { return res; })
    );
  }

}
