import axios, { AxiosInstance } from 'axios'
import { AxiosConfig } from '../constants/ApiConstants'
import { GetAccessToken } from './TokenHelper'

const AxiosAuthInstance: AxiosInstance = axios.create({
    headers: {
        'Content-Type': 'application/json'
    }
})

const exponentialDelay = (retryLeft: number): number => {
    const retryingFor = AxiosConfig.retry - retryLeft
    return AxiosConfig.firstRetryAt * 2 ** retryingFor
}

AxiosAuthInstance.interceptors.request.use(
    async (axiosConfig) => {
        try {
            const bearerToken = GetAccessToken();
            if (bearerToken) {
                axiosConfig.headers['Authorization'] = `Bearer ${bearerToken}`;
            }
            return axiosConfig;
        } catch (error) {
            if (error instanceof Error) {
                throw axiosConfig;
            }
        }
        return Promise.reject(Error);
    }
);

AxiosAuthInstance.interceptors.response.use(
    (response) => response,
    async (error) => {
        const { config, response: { status } } = error

        // if 401 then navigate to expired page
        if (status === 401) {
            window.location.href = '/expired'
        }

        if (!config?.retry || status < 500) {
            return Promise.reject(error);
        }
        config.retry -= 1
        await new Promise<void>((resolve) => {
            setTimeout(() => {
                resolve();
            }, exponentialDelay(config.retry));
        });
        return AxiosAuthInstance(config);
    }
)

export default AxiosAuthInstance
