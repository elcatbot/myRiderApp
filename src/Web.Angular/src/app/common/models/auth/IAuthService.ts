import { BehaviorSubject } from "rxjs";
import { IAuthStatus } from "./IAuthStatus";

export interface IAuthService {
    readonly authStatus$: BehaviorSubject<IAuthStatus>;
    login(email: string, password: string): void;
    register(fullName: string, role: string, email: string, password: string): void;
}