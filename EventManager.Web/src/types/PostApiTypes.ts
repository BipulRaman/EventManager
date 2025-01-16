type BasePostPayload = {
    title: string;
    content: string;
    image: string;
}

export type PostResult = BasePostPayload & {
    id: string;
    createdAt: string;
    modifiedAt: string;
    createdBy: string;
    createdByName: string;
    modifiedBy: string;
}

export type CreatePostPayload = BasePostPayload;

export type UpdatePostPayload = BasePostPayload & {
    id: string;
}
