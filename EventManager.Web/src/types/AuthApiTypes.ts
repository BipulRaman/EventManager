import { Roles } from "./ApiTypes";

export type AuthTokenResult = {
	tokenType: string;
	expiresIn: number;
	extExpiresIn: number;
	expiresOn: number;
	notBefore: number;
	resource: string;
	accessToken: string;
	name: string;
	userId: string;
	roles: Roles[];
}

export type CreateTokenPayload = {
    email: string;
    otp: string;
}

export type CreateOtpPayload = {
    email: string;
}