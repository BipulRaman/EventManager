import axios, { AxiosInstance } from 'axios'
import { AxiosConfig } from '../constants/ApiConstants'

const AxiosUnAuthInstance: AxiosInstance = axios.create({
  headers: {
    'Content-Type': 'application/json'
  }
})

const exponentialDelay = (retryLeft: number): number => {
  const retryingFor = AxiosConfig.retry - retryLeft;
  return AxiosConfig.firstRetryAt * 2 ** retryingFor;
}

AxiosUnAuthInstance.interceptors.request.use(
  async (axiosConfig) => {
    try {
      return axiosConfig;
    } 
    catch (error) {
      if (error instanceof Error) {
        throw axiosConfig;
      }
    }
    return Promise.reject(Error);
  }
);

AxiosUnAuthInstance.interceptors.response.use(
  (response) => response,
  (error) => {
    const { config, response: { status } } = error;

    if (!config?.retry || status < 500) {
      return Promise.reject(error);
    }
    config.retry -= 1;
    const delayRetryRequest = new Promise<void>((resolve) => {
      setTimeout(() => {
        resolve();
      }, exponentialDelay(config.retry));
    })
    return delayRetryRequest.then(() => AxiosUnAuthInstance(config));
  }
)

export default AxiosUnAuthInstance
