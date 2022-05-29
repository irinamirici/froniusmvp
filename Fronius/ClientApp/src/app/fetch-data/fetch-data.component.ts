import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { tap } from 'rxjs/operators';
import { PhotovoltaicSystem } from '../models/photovoltaic-system';
import { PhotovoltaicSystemDataSource } from '../services/photovoltaic-systems.datasource';
import { PhotovoltaicSystemService } from '../services/photovoltaic-systems.service';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent implements AfterViewInit, OnInit {
  public systems: PhotovoltaicSystem[] = [];

  dataSource!: PhotovoltaicSystemDataSource;
  displayedColumns = ["id", "name", "peakPower", "energyProducedCurrentDay"];

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(private systemsService: PhotovoltaicSystemService) { }

  ngOnInit() {
    this.dataSource = new PhotovoltaicSystemDataSource(this.systemsService);
    this.dataSource.loadPhotovoltaicSystems();
  }

  ngAfterViewInit() {
    this.paginator.page
      .pipe(
        tap(() => this.loadSystemsPage())
      )
      .subscribe();
  }

  loadSystemsPage() {
    this.dataSource.loadPhotovoltaicSystems(
      '',
      'asc',
      this.paginator.pageIndex,
      this.paginator.pageSize);
  }

  onRowClicked(row: PhotovoltaicSystem) {
    console.log('Row clicked: ', row);
  }
}
