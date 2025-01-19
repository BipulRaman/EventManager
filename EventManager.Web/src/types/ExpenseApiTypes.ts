type BaseEventPayload = {
    title: string,
    amount: number,
    dateTime: string // This should be in ISO 8601 format to be compatible with DateTimeOffset in C#
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
