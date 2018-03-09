export interface User {
    id: string;
    email: string;
    userName: string;
    password: string;
    confirmPassword: string;
    firstName: string;
    lastName: string;
    middleName: string;
    roles: string[];
}