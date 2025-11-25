export interface IAuthStatus {
    isAuthenticated: boolean;
    userId?: string;
    userRole?: string;
    role?: 'Rider' | 'Driver' | 'Admin | Guest';
}