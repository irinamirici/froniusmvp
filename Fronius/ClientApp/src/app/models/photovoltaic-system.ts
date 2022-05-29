export interface PhotovoltaicSystem {
  id: string;
  name: string;
  peakPower: number;
  energyProducedCurrentDay: number;
}

export interface Paged<T> {
  totalCount: number;
  items: T[];
}
