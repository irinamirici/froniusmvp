export interface PhotovoltaicSystem {
  id: string;
  name: string;
  peakPower: number;
  energyProducedCurrentDay: number;
}

export interface PhotovoltaicSystemDetail {
  id: string;
  name: string;
  peakPower: number;
  energyProducedCurrentDay: number;
  installationDate: Date;
  owner: string;
  address: Address;
  systemCondition: Condition;
}

export interface Address {
  latitude: number;
  longitude: number;
  countryCode: string;
  countryName: string;
  street: string;
  zipCode: string;
}

export enum Condition {
  Good = 0,
  Warning = 1,
  Error = 2
}

