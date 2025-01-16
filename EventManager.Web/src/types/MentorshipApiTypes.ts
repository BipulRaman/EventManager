type BaseMentorship = {
    subject: string,
    title: string,
    message: string,
    contact: string
}

export type MentorshipResult = BaseMentorship & {
    id: string,
    createdAt: string,
    modifiedAt: string,
    createdBy: string,
    createdByName: string,
    modifiedBy: string
}

export type CreateMentorshipPayload = BaseMentorship;

export type UpdateMentorshipPayload = BaseMentorship & {
    id: string
}
