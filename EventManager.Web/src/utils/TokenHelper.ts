import { Roles } from "../types/ApiTypes";
import { AuthTokenResult } from "../types/AuthApiTypes";

const isBrowser = typeof localStorage !== 'undefined';

export const SetAccessTokenData = (authTokenResult: AuthTokenResult): void => {
	if (isBrowser) {
		localStorage.setItem('authData', JSON.stringify(authTokenResult));
	}
}

export const ClearAccessTokenData = (): void => {
	if (isBrowser) {
		localStorage.removeItem('authData');
	}
}

export const GetAccessTokenData = (): AuthTokenResult => {
	if (isBrowser) {
		const authTokenResult: AuthTokenResult = JSON.parse(
			localStorage.getItem('authData') ?? '{}'
		) as AuthTokenResult;
		return authTokenResult;
	}
	return {} as AuthTokenResult;
}

export const GetAccessToken = (): string => {
	const authTokenResult: AuthTokenResult = GetAccessTokenData();
	return authTokenResult ? authTokenResult.accessToken : '';
}

export const IsTokenValid = (): boolean => {
	const authTokenResult: AuthTokenResult = GetAccessTokenData();
	if (authTokenResult) {
		const currentUnixTimestamp = Date.now() / 1000;
		if (authTokenResult.expiresOn && authTokenResult.expiresOn > currentUnixTimestamp) {
			return true;
		}
		if (isBrowser) {
			delete localStorage['authData'];
		}
		return false;
	} else return false;
}

export const GetUserRoles = (): Roles[] => {
	const authTokenResult: AuthTokenResult = GetAccessTokenData();
	return authTokenResult ? authTokenResult.roles as Roles[] : [];
}

export const GetUserEmail = (): string => {
	const authTokenResult: AuthTokenResult = GetAccessTokenData();
	return authTokenResult ? authTokenResult.resource : '';
}

export const GetUserName = (): string => {
	const authTokenResult: AuthTokenResult = GetAccessTokenData();
	return authTokenResult ? authTokenResult.name : '';
}

export const GetUserId = (): string => {
	const authTokenResult: AuthTokenResult = GetAccessTokenData();
	return authTokenResult ? authTokenResult.userId : '';
}
