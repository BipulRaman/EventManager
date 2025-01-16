export type ApiResponse<T = any> = {
	status: number
	errorCode: number
	result: T
}

export enum Roles {
	SuperAdmin = 'SuperAdmin',
	Admin = 'Admin',
	User = 'User',
	InvitedUser = 'InvitedUser',
	Creator = 'Creator',
}

export enum ProfileType {
	Alumni = 'Alumni',
	Student = 'Student',
	Staff = 'Staff',
	ExStaff = 'ExStaff',
}

export enum CallStatus {
	NotStarted = 'NotStarted',
	InProgress = 'InProgress',
	Success = 'Success',
	Failure = 'Failure',
}

export enum Gender {
	Male = 'Male',
	Female = 'Female',
	Other = 'Other',
}

export type SchoolData = {
	SchoolName: string
	StatePrefix: string
	UniqueId: number
}

export const Validator = {
	IsText: (text: string) => {
		return (text !== undefined && text !== null && text !== '');
	},

	IsNumber: (number: string) => {
		const numberRegex = new RegExp(/^[0-9]+$/);
		return numberRegex.test(number);
	},

	IsEmail: (email: string) => {
		const emailRegex = new RegExp(/^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/);
		return emailRegex.test(email);
	},

	IsPhone: (phone: string) => {
		const phoneRegex = new RegExp(/^[+]?[0-9]{10,13}$/);
		return phoneRegex.test(phone);
	},

	IsWebsite: (website: string) => {
		const websiteRegex = new RegExp(/^(http|https):\/\/[^ "]+$/);
		return websiteRegex.test(website);
	},

	IsGuid: (guid: string) => {
		const guidRegex = new RegExp(/^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$/);
		return guidRegex.test(guid);
	},
}