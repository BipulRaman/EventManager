type BaseNotification = {
  id: string,
  title: string,
  message: string
}

export type NotificationResult = BaseNotification & {
  createdAt: string,
  modifiedAt: string,
  createdBy: string,
  createdByName: string,
  modifiedBy: string
}

export type CreateNotificationPayload = BaseNotification;

export type UpdateNotificationPayload = BaseNotification;
