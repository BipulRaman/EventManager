export const AxiosConfig = {
	retry: 5,
	firstRetryAt: 200
}

export const ApiBasePath = 'https://alumniapp.azurewebsites.net';
export const EventId = '72aef99a-cdec-4176-9c3a-0acc493f29b2';

export const AuthEndPoints = {
	PostAuthOtp: () => `${ApiBasePath}/auth/otp`,
	PostAuthToken: () => `${ApiBasePath}/auth/token`,
	GetAuthReset: () => `${ApiBasePath}/auth/reset`,
	GetAuthOtpSecret: () => `${ApiBasePath}/auth/otpsecret`,
}

export const UserEndPoints = {
	GetUserById: (id: string) => `${ApiBasePath}/user/${id}`,
	PostUser: () => `${ApiBasePath}/user`,
	PatchUser: () => `${ApiBasePath}/user`,
	DeleteUser: (id: string) => `${ApiBasePath}/user/${id}`
}

export const ProfileEndPoints = {
	GetProfile: () => `${ApiBasePath}/profile`,
	GetProfileById: (id: string) => `${ApiBasePath}/profile/${id}`,
	PostProfile: () => `${ApiBasePath}/profile`,
	PatchProfile: () => `${ApiBasePath}/profile`,
	PostProfilePhoto: () => `${ApiBasePath}/profile/photo`,
	GetProfilesNearby: (radiusInKm: number) => `${ApiBasePath}/profile/nearby/${radiusInKm}`,
	GetProfilesByPhone: (phone: string) => `${ApiBasePath}/profile/phone/${phone}`,
	PatchProfileGeo: (lat: number, lon: number) => `${ApiBasePath}/profile/geo/${lat}/${lon}`,
	CheckInVenue: (id: string) => `${ApiBasePath}/profile/checkin/venue/${id}`,
	CheckInGift: (id: string) => `${ApiBasePath}/profile/checkin/gift/${id}`,
	CheckInMeal: (id: string) => `${ApiBasePath}/profile/checkin/meal/${id}`,
}

export const AttendeeEndPoints = {
	GetProfile: () => `${ApiBasePath}/attendee`,
	GetProfileById: (id: string) => `${ApiBasePath}/attendee/${id}`,
	PostProfile: () => `${ApiBasePath}/attendee`,
	PatchProfile: () => `${ApiBasePath}/attendee`,
	PostProfilePhoto: () => `${ApiBasePath}/attendee/photo`,
	GetProfilesNearby: (radiusInKm: number) => `${ApiBasePath}/attendee/nearby/${radiusInKm}`,
	GetProfilesByPhone: (phone: string) => `${ApiBasePath}/attendee/phone/${phone}`,
	PatchProfileGeo: (lat: number, lon: number) => `${ApiBasePath}/attendee/geo/${lat}/${lon}`,
	CheckInVenue: (id: string) => `${ApiBasePath}/attendee/checkin/venue/${id}`,
	CheckInGift: (id: string) => `${ApiBasePath}/attendee/checkin/gift/${id}`,
	CheckInMeal: (id: string) => `${ApiBasePath}/attendee/checkin/meal/${id}`,
}

export const BusinessEndPoints = {
	GetBusinesses: () => `${ApiBasePath}/business`,
	GetBusinessesByPincode: (pincode: number) => `${ApiBasePath}/business/nearby/${pincode}`,
	GetBusinessByPinCodeCategory: (pincode: number, category: string) => `${ApiBasePath}/business/nearby/${pincode}/${category}`,
	GetBusinessById: (id: string) => `${ApiBasePath}/business/${id}`,
	PostBusiness: () => `${ApiBasePath}/business`,
	PatchBusiness: () => `${ApiBasePath}/business`,
	DeleteBusiness: (id: string) => `${ApiBasePath}/business/${id}`
}

export const EventEndPoints = {
	eventId: 'f453d583-5434-432d-b06c-803c09d72207',
	GetEvents: () => `${ApiBasePath}/event`,
	GetEventById: (id: string) => `${ApiBasePath}/event/${id}`,
	PostEvent: () => `${ApiBasePath}/event`,
	PatchEvent: () => `${ApiBasePath}/event`,
	DeleteEvent: (id: string) => `${ApiBasePath}/event/${id}`
}

export const MentorshipEndPoints = {
	GetMentorships: () => `${ApiBasePath}/mentorship`,
	GetMentorshipById: (id: string) => `${ApiBasePath}/mentorship/${id}`,
	PostMentorship: () => `${ApiBasePath}/mentorship`,
	PatchMentorship: () => `${ApiBasePath}/mentorship`,
	DeleteMentorship: (id: string) => `${ApiBasePath}/mentorship/${id}`
}

export const NotificationEndPoints = {
	GetNotifications: () => `${ApiBasePath}/notification`,
	GetNotificationById: (id: string) => `${ApiBasePath}/notification/${id}`,
	PostNotification: () => `${ApiBasePath}/notification`,
	PatchNotification: () => `${ApiBasePath}/notification`,
	DeleteNotification: (id: string) => `${ApiBasePath}/notification/${id}`
}

export const PostEndPoints = {
	GetPublicPosts: (pageNumber: number) => `${ApiBasePath}/post/public/20/${pageNumber}`,
	GetPosts: (pageNumber: number) => `${ApiBasePath}/post/20/${pageNumber}`,
	GetPostById: (id: string) => `${ApiBasePath}/post/${id}`,
	PostPost: () => `${ApiBasePath}/post`,
	PatchPost: () => `${ApiBasePath}/post`,
	DeletePost: (id: string) => `${ApiBasePath}/post/${id}`
}

export const ExpenseEndPoints = {
	GetExpenses: () => `${ApiBasePath}/expense/${EventId}`,
	GetExpenseById: (id: string) => `${ApiBasePath}/expense/${EventId}/${id}`,
	PostExpense: () => `${ApiBasePath}/expense/${EventId}`,
	PatchExpense: () => `${ApiBasePath}/expense/${EventId}`,
	DeleteExpense: (id: string) => `${ApiBasePath}/expense/${EventId}/${id}`
}
