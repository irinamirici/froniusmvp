import { Component, OnInit, ViewChild } from "@angular/core";
import { ActivatedRoute, Params } from "@angular/router";
import { Condition, PhotovoltaicSystemDetail } from "../models/photovoltaic-system";
import { PhotovoltaicSystemService } from "../services/photovoltaic-systems.service";
import { MatAccordion } from '@angular/material/expansion';

@Component({
  selector: 'app-system-detail',
  templateUrl: './system-detail.component.html',
  styleUrls: ['system-detail.css'],
})
export class SystemDetailComponent implements OnInit {
  system!: PhotovoltaicSystemDetail;
  systemId!: string;
  Condition = Condition;

  @ViewChild(MatAccordion) accordion!: MatAccordion;

  constructor(private route: ActivatedRoute,
    private systemService: PhotovoltaicSystemService) { }

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => this.systemId = params['id']);
    console.log("system details " + this.systemId);
    this.systemService
      .findSystemById(this.systemId)
      .subscribe(res => this.system = res);
  }
}
