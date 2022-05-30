import { CollectionViewer, DataSource } from "@angular/cdk/collections";
import { Observable, BehaviorSubject, of } from "rxjs";
import { catchError, finalize } from "rxjs/operators";
import { Paged } from "../models/paged";
import { PhotovoltaicSystem } from "../models/photovoltaic-system";
import { PhotovoltaicSystemService } from "./photovoltaic-systems.service";

export class PhotovoltaicSystemDataSource implements DataSource<PhotovoltaicSystem> {

  private photovoltaicSystemSubject = new BehaviorSubject<PhotovoltaicSystem[]>([]);
  private loadingSubject = new BehaviorSubject<boolean>(false);
  private totalCountSubject = new BehaviorSubject<number>(0);

  public loading$ = this.loadingSubject.asObservable();
  public totalCount$ = this.totalCountSubject.asObservable();

  constructor(private systemsService: PhotovoltaicSystemService) { }

  connect(collectionViewer: CollectionViewer): Observable<PhotovoltaicSystem[]> {
    return this.photovoltaicSystemSubject.asObservable();
  }

  disconnect(collectionViewer: CollectionViewer): void {
    this.photovoltaicSystemSubject.complete();
    this.loadingSubject.complete();
  }

  loadPhotovoltaicSystems(sortBy = '', sortDirection = 'asc', pageIndex = 0, pageSize = 3) {

    this.loadingSubject.next(true);

    this.systemsService.findPhotovoltaicSystems(sortBy, sortDirection, pageIndex, pageSize)
      .pipe(
        catchError(() => of([])),
        finalize(() => this.loadingSubject.next(false))
      )
      .subscribe((pagedSystems: Paged<PhotovoltaicSystem>) => {
        this.totalCountSubject.next(pagedSystems.totalCount);
        this.photovoltaicSystemSubject.next(pagedSystems.items);
      });
  }
}
