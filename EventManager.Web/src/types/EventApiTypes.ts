type BaseEventPayload = {
    title: string,
    details: string,
    startDateTime: string,
    endDateTime: string,
    location: string,
    link: string
}

export type EventResult = BaseEventPayload & {
    id: string,
    createdAt: string,
    modifiedAt: string,
    createdBy: string,
    createdByName: string,
    modifiedBy: string
}

export type CreateEventPayload = BaseEventPayload;

export type UpdateEventPayload = BaseEventPayload & {
    id: string
}
