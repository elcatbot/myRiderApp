import { IFare } from "./IFare";
import { ILocation } from "./ILocation";

export interface IRide {
    id: string;
    riderId: string;
    driverId?: string;
    fare?: IFare
    pickUp: ILocation;
    dropOff: ILocation;
    status: 'Requested' | 'Accepted' | 'InProgress' | 'Completed' | 'Cancelled';
    requestedAt: Date;
}