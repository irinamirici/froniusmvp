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
  owner: string;
  address: Address;
}

export interface Address {
  latitude: number;
  longitude: number;
  countryCode: string;
  countryName: string;
  street: string;
}

