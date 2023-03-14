import axios, { AxiosError, type AxiosRequestConfig, type AxiosResponse } from 'axios';
import { API_URL } from '../../app.config';

const errorHandler = async (error: AxiosError) => {
    // store.commit('SET_LOADING', false)

    const config: AxiosRequestConfig | undefined = error?.config
    // if (error.response?.status === 401) {
    // const accessToken = await RefreshToken();
    // config.headers = {
    //     ...config.headers,
    //     Authorization: `Bearer ${accessToken}`
    // }

    // return axios(config)

    // }

    // return axios(config)
    return Promise.reject(error)

}
const requestHandler = (request: AxiosRequestConfig) => {
    //  console.log(request)
    //     if (request.headers)
    //         request.headers['Authorization'] = `Bearer ${GetAccessToken()}`;
    return request;
};
const responseHandler = (response: AxiosResponse) => {



    return response;


};

const axiosIns = axios.create({
    baseURL: API_URL,
    withCredentials:false,
    headers: {
        'Access-Control-Allow-Origin': '*',
    }
});

axiosIns.interceptors.request.use(requestHandler)
axiosIns.interceptors.response.use(responseHandler, errorHandler)




export { axiosIns };