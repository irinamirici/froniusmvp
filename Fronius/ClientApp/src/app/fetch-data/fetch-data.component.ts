import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { merge } from 'rxjs/internal/observable/merge';
import { tap } from 'rxjs/operators';
import { PhotovoltaicSystem } from '../models/photovoltaic-system';
import { PhotovoltaicSystemDataSource } from '../services/photovoltaic-systems.datasource';
import { PhotovoltaicSystemService } from '../services/photovoltaic-systems.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent implements AfterViewInit, OnInit {
  public systems: PhotovoltaicSystem[] = [];

  dataSource!: PhotovoltaicSystemDataSource;
  displayedColumns = ["id", "name", "peakPower", "energyProducedCurrentDay"];

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private systemsService: PhotovoltaicSystemService, private router: Router) { }

  ngOnInit() {
    this.dataSource = new PhotovoltaicSystemDataSource(this.systemsService);
    this.dataSource.loadPhotovoltaicSystems();
  }

  ngAfterViewInit() {
    // reset the paginator after sorting
    this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 0);

    merge(this.sort.sortChange, this.paginator.page)
      .pipe(
        tap(() => this.loadSystemsPage())
      )
      .subscribe();
  }

  loadSystemsPage() {
    console.log(this.sort.active + " " + this.sort.direction);
    this.dataSource.loadPhotovoltaicSystems(
      this.sort.direction != '' ? this.sort.active : '',
      this.sort.direction,
      this.paginator.pageIndex,
      this.paginator.pageSize);
  }

  onRowClicked(row: PhotovoltaicSystem) {
    console.log('Row clicked: ', row);
    this.router.navigate(['/system', row.id]);
  }
}
