import { Roles } from "./ApiTypes";

export type User = {
    id: string;
    tenantId: string;
    name: string;
    email: string;
    phone: string;
    roles: Roles[];
    createdAt: string;
}

export type UserCreatePayload = {
    name: string;
    email: string;
    phone: string;
}

export type UserUpdatePayload = {
    name: string;
    email: string;
    phone: string;
}


// create interface with initial value
export interface test {
    name: string;
    age: number;
}