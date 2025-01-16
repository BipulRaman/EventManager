type BusinessBase = {
    name: string,
    details: string,
    category: string,
    address: string,
    pinCode: number,
    mapLink: string,
    latitude: number,
    longitude: number,
    phone: string,
    email: string,
    website: string
}

export type BusinessResult = BusinessBase & {
    id: string,
    createdAt: string,
    modifiedAt: string,
    createdBy: string,
    createdByName: string,
    modifiedBy: string
}

export type CreateBusinessPayload = BusinessBase;

export type UpdateBusinessPayload = BusinessBase & {
    id: string
}