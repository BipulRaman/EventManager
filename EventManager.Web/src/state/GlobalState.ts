import { create } from 'zustand';
import { PostResult } from "../types/PostApiTypes";
import { ProfileResult, PublicProfileResult } from "../types/ProfileApiTypes";
import { CallStatus } from "../types/ApiTypes";
import { BusinessResult } from '@/types/BusinessApiTypes';

export type StateData<T> = {
    data: T;
    status: CallStatus;
    timestamp: Date;
}

export type StateDataPage<T> = {
    data: T;
    page: number;
    isLastPage: boolean;
    status: CallStatus;
    timestamp: Date;
}

type GlobalState = {
    otpSecretState: StateData<string>;
    publicPostListState: StateDataPage<PostResult[]>;
    privatePostListState: StateDataPage<PostResult[]>;
    profileState: StateData<ProfileResult>;
    publicProfileState: StateData<PublicProfileResult>;
    nearbyProfileListState: StateData<PublicProfileResult[]>;
    nearbyBusinessListState: StateData<BusinessResult[]>;
    nearbyDoctorsListState: StateData<BusinessResult[]>;    
    businessState: StateData<BusinessResult>;
    businessStateList: StateData<BusinessResult[]>;
    setOtpSecretState: (state: StateData<string>) => void;
    setPublicPostListState: (state: StateDataPage<PostResult[]>) => void;
    setPrivatePostListState: (state: StateDataPage<PostResult[]>) => void;
    setProfileState: (state: StateData<ProfileResult>) => void;
    setPublicProfileState: (state: StateData<PublicProfileResult>) => void;
    setNearbyProfileListState: (state: StateData<PublicProfileResult[]>) => void;
    setNearbyBusinessListState: (state: StateData<BusinessResult[]>) => void;
    setNearbyDoctorsListState: (state: StateData<BusinessResult[]>) => void;
    setBusinessState: (state: StateData<BusinessResult>) => void;
    setBusinessStateList: (state: StateData<BusinessResult[]>) => void;
}

const initialStateData = <T>(data: T): StateData<T> => ({
    data,
    status: CallStatus.NotStarted,
    timestamp: new Date(),
});

const initialStateDataPage = <T>(data: T): StateDataPage<T> => ({
    data,
    page: 0,
    isLastPage: false,
    status: CallStatus.NotStarted,
    timestamp: new Date(),
});

const useStore = create<GlobalState>()((set) => ({
    otpSecretState: initialStateData(""),
    publicPostListState: initialStateDataPage([] as PostResult[]),
    privatePostListState: initialStateDataPage([] as PostResult[]),
    profileState: initialStateData({} as ProfileResult),
    publicProfileState: initialStateData({} as PublicProfileResult),
    nearbyProfileListState: initialStateData([] as PublicProfileResult[]),
    businessState: initialStateData({} as BusinessResult),
    businessStateList: initialStateData([] as BusinessResult[]),
    nearbyBusinessListState: initialStateData([] as BusinessResult[]),
    nearbyDoctorsListState: initialStateData([] as BusinessResult[]),
    setOtpSecretState: (state: StateData<string>) => set(() => ({ otpSecretState: state })),
    setPublicPostListState: (state: StateDataPage<PostResult[]>) => set(() => ({ publicPostListState: state })),
    setPrivatePostListState: (state: StateDataPage<PostResult[]>) => set(() => ({ privatePostListState: state })),
    setProfileState: (state: StateData<ProfileResult>) => set(() => ({ profileState: state })),
    setPublicProfileState: (state: StateData<PublicProfileResult>) => set(() => ({ publicProfileState: state })),
    setNearbyProfileListState: (state: StateData<PublicProfileResult[]>) => set(() => ({ nearbyProfileListState: state })),
    setBusinessState: (state: StateData<BusinessResult>) => set(() => ({ businessState: state })),
    setBusinessStateList: (state: StateData<BusinessResult[]>) => set(() => ({ businessStateList: state })),
    setNearbyBusinessListState: (state: StateData<BusinessResult[]>) => set(() => ({ nearbyBusinessListState: state })),
    setNearbyDoctorsListState: (state: StateData<BusinessResult[]>) => set(() => ({ nearbyDoctorsListState: state })),
}));

export const ResetAllStores = () => {
    useStore.setState(() => ({
        otpSecretState: initialStateData(""),
        publicPostListState: initialStateDataPage([] as PostResult[]),
        privatePostListState: initialStateDataPage([] as PostResult[]),
        profileState: initialStateData({} as ProfileResult),
        publicProfileState: initialStateData({} as PublicProfileResult),
        nearbyProfileListState: initialStateData([] as PublicProfileResult[]),
        businessState: initialStateData({} as BusinessResult),
        businessStateList: initialStateData([] as BusinessResult[]),
        nearbyBusinessListState: initialStateData([] as BusinessResult[]),
        nearbyDoctorsListState: initialStateData([] as BusinessResult[]),
    }));
};

export default useStore;
