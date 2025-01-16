import { CallStatus, ApiResponse } from "@/types/ApiTypes";
import { StateData } from "@/state/GlobalState";
import { Dispatch, SetStateAction } from "react";

export const ApiGlobalStateManager = async <T,>(apiRequest: Promise<{ data: ApiResponse<T> }>, setGlobalState: (state: StateData<T>) => void) => {
    setGlobalState({
        data: {} as T,
        status: CallStatus.InProgress,
        timestamp: new Date(),
    });

    try {
        const response = await apiRequest;
        const apiResult = response.data as ApiResponse<T>;
        setGlobalState({
            data: apiResult.result,
            status: CallStatus.Success,
            timestamp: new Date(),
        });
    } catch {
        setGlobalState({
            data: {} as T,
            status: CallStatus.Failure,
            timestamp: new Date(),
        });
    }
}

export const ApiComponentStateManager = async <T,>(apiRequest: Promise<{ data: ApiResponse<T> }>, setComponentState: Dispatch<SetStateAction<StateData<T>>>) => {
    setComponentState({
        data: {} as T,
        status: CallStatus.InProgress,
        timestamp: new Date(),
    });

    try {
        const response = await apiRequest;
        const apiResult = response.data as ApiResponse<T>;
        setComponentState({
            data: apiResult.result,
            status: CallStatus.Success,
            timestamp: new Date(),
        });
    } catch {
        setComponentState({
            data: {} as T,
            status: CallStatus.Failure,
            timestamp: new Date(),
        });
    }
}
