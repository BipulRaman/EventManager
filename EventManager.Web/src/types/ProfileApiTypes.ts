import { ProfileType, Roles } from "./ApiTypes";

export type PublicProfileResult = {
    id: string;
    name: string;
    gender: string;
    isContactInfoVisible: boolean;
    email: string;
    phone: string;
    location: string;
    photo: string;
    profileType: string;
    entryYear: number;
    entryClass: number;
    exitYear: number;
    exitClass: number;
    primarySchoolId: number;
    primarySchoolName: string;
    secondarySchoolIds: number[];
    secondarySchoolNames: string;
    isHigherEducationInfoVisible: boolean;
    isStudying: boolean;
    university: string;
    degree: string;
    isEmploymentInfoVisible: boolean;
    isWorking: boolean;
    organization: string;
    jobTitle: string;
}

export type ProfileResult = PublicProfileResult & {
    createdAt: string;
    modifiedAt: string;
    createdBy: string;
    createdByName: string;
    modifiedBy: string;
    latitude: number;
    longitude: number;
    roles: Roles[];
}

export type BaseProfilePayload = {
    name: string;
    gender: string;
    isContactInfoVisible: boolean;
    phone: string;
    location: string;
    latitude: number;
    longitude: number;
    profileType: ProfileType;
    entryYear: number;
    entryClass: number;
    exitYear: number;
    exitClass: number;
    primarySchoolId: number;
    secondarySchoolIds: number[];
    isHigherEducationInfoVisible: boolean;
    isStudying: boolean;
    university: string;
    degree: string;
    isEmploymentInfoVisible: boolean;
    isWorking: boolean;
    organization: string;
    jobTitle: string;
}

export type CreateProfilePayload = BaseProfilePayload & {
    email: string;
}

export type UpdateProfilePayload = BaseProfilePayload & {
    id: string;
}

export type ProfileGeoUpdatePayload = {
    latitude: number;
    longitude: number;
}

const castBaseProfilePayload = (data: ProfileResult): BaseProfilePayload => ({
    name: data.name ?? '',
    gender: data.gender ?? '',
    isContactInfoVisible: data.isContactInfoVisible ?? false,
    phone: data.phone ?? '',
    location: data.location ?? '',
    latitude: data.latitude ?? 0,
    longitude: data.longitude ?? 0,
    profileType: data.profileType as ProfileType ?? ProfileType.Alumni,
    entryYear: data.entryYear ?? 0,
    entryClass: data.entryClass ?? 0,
    exitYear: data.exitYear ?? 0,
    exitClass: data.exitClass ?? 0,
    primarySchoolId: data.primarySchoolId ?? 0,
    secondarySchoolIds: data.secondarySchoolIds ?? [],
    isHigherEducationInfoVisible: data.isHigherEducationInfoVisible ?? false,
    isStudying: data.isStudying ?? false,
    university: data.university ?? '',
    degree: data.degree ?? '',
    isEmploymentInfoVisible: data.isEmploymentInfoVisible ?? false,
    isWorking: data.isWorking ?? false,
    organization: data.organization ?? '',
    jobTitle: data.jobTitle ?? '',
});

export const CastedUpdateProfilePayload = (data: ProfileResult): UpdateProfilePayload => ({
    ...castBaseProfilePayload(data),
    id: data.id ?? '',
});

export const CastedCreateProfilePayload = (data: ProfileResult): CreateProfilePayload => ({
    ...castBaseProfilePayload(data),
    email: data.email ?? '',
});