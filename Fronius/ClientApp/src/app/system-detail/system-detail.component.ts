import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Params } from "@angular/router";
import { PhotovoltaicSystemService } from "../services/photovoltaic-systems.service";

@Component({
  selector: 'app-system-detail',
  templateUrl: './system-detail.component.html'
})
export class SystemDetailComponent implements OnInit {
  system: any;
  systemId: string = "dd";
  constructor(private route: ActivatedRoute,
    private systemService: PhotovoltaicSystemService) { }

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => this.systemId = params['id']);
    console.log("system " + this.systemId);
    this.systemService
      .findSystemById(this.systemId)
      .subscribe(res => this.system = res);
    }
}
