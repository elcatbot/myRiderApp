import { IRide } from "./IRide";

export interface IRidePageIndex { 
    pageIndex: number;
    data: IRide[];
    pageSize: number;
    count: number;
}