import { CreateOtpPayload, CreateTokenPayload } from "@/types/AuthApiTypes";
import { AuthEndPoints, BusinessEndPoints, NotificationEndPoints, PostEndPoints, ProfileEndPoints, UserEndPoints } from "../constants/ApiConstants";
import { CreateBusinessPayload } from "../types/BusinessApiTypes";
import AxiosAuthInstance from "../utils/AxiosAuthConfig";
import { CreateProfilePayload, UpdateProfilePayload } from "@/types/ProfileApiTypes";
import { CreateNotificationPayload } from "@/types/NotificationApiTypes";
import { CreatePostPayload } from "@/types/PostApiTypes";
import { UserCreatePayload, UserUpdatePayload } from "@/types/UserApiTypes";

enum HttpMethod {
    GET = 'get',
    POST = 'post',
    POST_FORM = 'post_form',
    PATCH = 'patch',
    DELETE = 'delete'
}

const createApiCall = async <T = unknown>(method: HttpMethod, endpoint: string, payload?: T) => {
    switch (method) {
        case HttpMethod.GET:
            return await AxiosAuthInstance.get(endpoint);
        case HttpMethod.POST:
            return await AxiosAuthInstance.post(endpoint, payload);
        case HttpMethod.POST_FORM:
            return await AxiosAuthInstance.post(endpoint, payload, {
                headers: {
                    'Content-Type': 'multipart/form-data',
                    'Content-Length': 'Infinity'
                }
            });
        case HttpMethod.PATCH:
            return await AxiosAuthInstance.patch(endpoint, payload);
        case HttpMethod.DELETE:
            return await AxiosAuthInstance.delete(endpoint);
        default:
            throw new Error(`Unsupported method: ${method}`);
    }
};

export const BusinessApi = {
    GetMyBusinessList: async () => createApiCall(HttpMethod.GET, BusinessEndPoints.GetBusinesses()),
    GetBusinessListByPinCode: async (pincode: number) => createApiCall(HttpMethod.GET, BusinessEndPoints.GetBusinessesByPincode(pincode)),
    GetBusinessListByPinCodeCategory: async (pincode: number, category: string) => createApiCall(HttpMethod.GET, BusinessEndPoints.GetBusinessByPinCodeCategory(pincode, category)),
    GetBusinessById: async (id: string) => createApiCall(HttpMethod.GET, BusinessEndPoints.GetBusinessById(id)),
    CreateBusiness: async (payload: CreateBusinessPayload) => createApiCall(HttpMethod.POST, BusinessEndPoints.PostBusiness(), payload),
    UpdateBusiness: async (payload: CreateBusinessPayload) => createApiCall(HttpMethod.PATCH, BusinessEndPoints.PatchBusiness(), payload),
    DeleteBusiness: async (id: string) => createApiCall(HttpMethod.DELETE, BusinessEndPoints.DeleteBusiness(id)),
};

export const AuthServices = {
    CreateToken: async (payload: CreateTokenPayload) => createApiCall(HttpMethod.POST, AuthEndPoints.PostAuthToken(), payload),
    CreateOtp: async (payload: CreateOtpPayload) => createApiCall(HttpMethod.POST, AuthEndPoints.PostAuthOtp(), payload),
    ResetAuth: async () => createApiCall(HttpMethod.GET, AuthEndPoints.GetAuthReset()),
    GetOtpSecret: async () => createApiCall(HttpMethod.GET, AuthEndPoints.GetAuthOtpSecret()),
}

export const UserServices = {
    GetUserById: async (id: string) => createApiCall(HttpMethod.GET, UserEndPoints.GetUserById(id)),
    CreateUser: async (payload: UserCreatePayload) => createApiCall(HttpMethod.POST, UserEndPoints.PostUser(), payload),
    UpdateUser: async (payload: UserUpdatePayload) => createApiCall(HttpMethod.PATCH, UserEndPoints.PatchUser(), payload),
    DeleteUser: async (id: string) => createApiCall(HttpMethod.DELETE, UserEndPoints.DeleteUser(id)),
}

export const ProfileServices = {
    GetProfile: async () => createApiCall(HttpMethod.GET, ProfileEndPoints.GetProfile()),
    GetProfileById: async (id: string) => createApiCall(HttpMethod.GET, ProfileEndPoints.GetProfileById(id)),
    CreateProfile: async (payload: CreateProfilePayload) => createApiCall(HttpMethod.POST, ProfileEndPoints.PostProfile(), payload),
    UpdateProfile: async (payload: UpdateProfilePayload) => createApiCall(HttpMethod.PATCH, ProfileEndPoints.PatchProfile(), payload),
    UpdateProfilePhoto: async (payload: FormData) => createApiCall(HttpMethod.POST_FORM, ProfileEndPoints.PostProfilePhoto(), payload),
    UpdateProfileGeo: async (lat: number, lon: number) => createApiCall(HttpMethod.PATCH, ProfileEndPoints.PatchProfileGeo(lat, lon)),
    GetProfilesNearby: async (radiusInKm: number) => createApiCall(HttpMethod.GET, ProfileEndPoints.GetProfilesNearby(radiusInKm)),
    GetProfilesByPhone: async (phone: string) => createApiCall(HttpMethod.GET, ProfileEndPoints.GetProfilesByPhone(phone)),
    CheckInVenue: async (id: string) => createApiCall(HttpMethod.POST, ProfileEndPoints.CheckInVenue(id)),
    CheckInGift: async (id: string) => createApiCall(HttpMethod.POST, ProfileEndPoints.CheckInGift(id)),
    CheckInMeal: async (id: string) => createApiCall(HttpMethod.POST, ProfileEndPoints.CheckInMeal(id)),
};
export const NotificationServices = {
    GetNotifications: async () => createApiCall(HttpMethod.GET, NotificationEndPoints.GetNotifications()),
    GetNotificationById: async (id: string) => createApiCall(HttpMethod.GET, NotificationEndPoints.GetNotificationById(id)),
    CreateNotification: async (payload: CreateNotificationPayload) => createApiCall(HttpMethod.POST, NotificationEndPoints.PostNotification(), payload),
    UpdateNotification: async (payload: CreateNotificationPayload) => createApiCall(HttpMethod.PATCH, NotificationEndPoints.PatchNotification(), payload),
    DeleteNotification: async (id: string) => createApiCall(HttpMethod.DELETE, NotificationEndPoints.DeleteNotification(id)),
}

export const PostServices = {
    GetPublicPosts: async (pageNumber: number) => createApiCall(HttpMethod.GET, PostEndPoints.GetPublicPosts(pageNumber)),
    GetPosts: async (pageNumber: number ) => createApiCall(HttpMethod.GET, PostEndPoints.GetPosts(pageNumber)),
    GetPostById: async (id: string) => createApiCall(HttpMethod.GET, PostEndPoints.GetPostById(id)),
    CreatePost: async (payload: CreatePostPayload) => createApiCall(HttpMethod.POST, PostEndPoints.PostPost(), payload),
    UpdatePost: async (payload: CreatePostPayload) => createApiCall(HttpMethod.PATCH, PostEndPoints.PatchPost(), payload),
    DeletePost: async (id: string) => createApiCall(HttpMethod.DELETE, PostEndPoints.DeletePost(id)),
}
