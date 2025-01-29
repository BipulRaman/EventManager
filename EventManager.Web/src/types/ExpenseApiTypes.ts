type BaseExpensePayload = {
    title: string,
    amount: number,
    dateTime: number // This should be in ISO 8601 format to be compatible with DateTimeOffset in C#
}

export type ExpenseResult = BaseExpensePayload & {
    id: string,
    createdAt: number,
    modifiedAt: number,
    createdBy: string,
    createdByName: string,
    modifiedBy: string
}

export type CreateExpensePayload = BaseExpensePayload;

export type UpdateExpensePayload = BaseExpensePayload & {
    id: string
}
