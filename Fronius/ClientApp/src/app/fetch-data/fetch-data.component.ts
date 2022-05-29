import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public systems: PhotovoltaicSystem[] = [];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<PhotovoltaicSystem[]>(baseUrl + 'photovoltaicsystems').subscribe(result => {
      this.systems = result;
    }, error => console.error(error));
  }
}

interface PhotovoltaicSystem {
  id: string;
  name: string;
  peakPower: number;
  energyProducedCurrentDay: number;
}
