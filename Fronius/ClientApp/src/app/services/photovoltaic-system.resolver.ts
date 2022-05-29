import { Injectable} from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot} from "@angular/router";
import { Observable} from "rxjs";
import { PhotovoltaicSystemDetail } from "../models/photovoltaic-system";
import { PhotovoltaicSystemService } from "./photovoltaic-systems.service";



@Injectable()
export class PhotovoltaicSystemResolver implements Resolve<PhotovoltaicSystemDetail> {

  constructor(private systemService: PhotovoltaicSystemService) { }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<PhotovoltaicSystemDetail> {
  return this.systemService.findSystemById(route.params['id']);
}

}
