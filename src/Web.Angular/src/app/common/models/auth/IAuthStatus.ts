export interface IAuthStatus {
    isAuthenticated: boolean;
    userId?: string;
    userRole?: string;
    role?: 'rider' | 'driver' | 'admin | guest';
}