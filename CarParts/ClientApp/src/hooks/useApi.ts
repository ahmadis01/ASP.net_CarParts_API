import { axiosIns } from '../libs/axios';
import Swal from 'sweetalert2';
import { SweetAlertOptions } from 'sweetalert2'
import { AxiosRequestConfig, AxiosError } from "axios"
import { serialize } from "object-to-formdata";


type NotificationsType = { error: string | boolean, comfirm?: SweetAlertOptions | boolean, success?: string | boolean }
type paramsType = { [param: string]: string }
const defaultSerializerOptions = { indices: true, dotsForObjectNotation: true, noFilesWithArrayNotation: true }


const defaultPostNotifications: NotificationsType = {
    comfirm: false,
    success: false,
    error: true
};

const defaultGetNotification: NotificationsType = {
    comfirm: false,
    success: false,
    error: true
};

const defaultDeleteNotification: NotificationsType = {
    comfirm: {
        text: 'سيتم حذف العنصر المحدد .. هل انت متأكد ؟ ',
        icon: 'warning',
        confirmButtonText: "نعم",
        denyButtonText: "تراجع"
    },
    error: true,
    success: "تم الحذف بنجاح",

}




export const useApi = () => {
    const handleErrorMessage = ({ response }: AxiosError | any, notifications: string | boolean) => {
        console.log('ERROR Handler');

        if (typeof (notifications) == 'boolean') {
            if (response.data.errors) {
                for (const key in response.data.errors) {
                    if (Object.prototype.hasOwnProperty.call(response.data.errors, key)) {
                        const errorType = response.data.errors[key];
                        if (typeof (errorType) == 'object' && Array.isArray(errorType)) {
                            errorType.forEach(msg => {
                                console.log('any')
                            })
                        }

                    }
                }
            }
            else if (response.data.message)
                console.log('any')
            else if (response.data.title)
                console.log('any')
            else if (response.message)
                console.log('any')      
                      else if (response.statusText)
                console.log('any')

        }
        else
            if (typeof (notifications) == 'string') {
                console.log('any')
            }

    }




    const GET = async <T>(url: string, params?: paramsType, notifications: NotificationsType = defaultGetNotification, config?: AxiosRequestConfig) => {
        try {
            const response = await axiosIns.get<T>(url, { params, ...config })
            return { ...response, data: response.data as T }
        }

        catch (error) {
            if (notifications.error)
                handleErrorMessage(error, notifications.error);

            throw (error)
        }

    };
    const POST = async (url: string, body: any = {}, notifications: NotificationsType = defaultPostNotifications, config: AxiosRequestConfig & { formData?: boolean } = {}) => {
        try {
            let response;
            if (config.formData) {
                response = await axiosIns.post(url, serialize(body, defaultSerializerOptions)
                    , { ...config })

            }
            else {
                response = await axiosIns.post(url, { ...body }, { ...config })
            }

            if (notifications.success && response.status == 200) {
                if (typeof (notifications.success) == 'string')
                    console.log('any')        
                            else
                    console.log('any')
            }


            return response
        }
        catch (error) {
            if (notifications.error) {
                console.log("ERROR AREA");

                handleErrorMessage(error, notifications.error);
            }
            throw (error)
        }
    };
    const DELETE = async (url: string, body: any, params?: paramsType[], notifications: NotificationsType = defaultDeleteNotification, config?: AxiosRequestConfig) => {
        return new Promise((reject, resolve) => {
            if (notifications.comfirm && typeof notifications.comfirm == 'object')
                Swal.fire({
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    ...notifications.comfirm

                }).then((result) => {
                    if (result.isConfirmed) {
                        axiosIns.delete(url, { ...params, ...config, data: body }).then((res) => {

                            if (res.status == 200 && typeof (notifications.success) == 'string') {
                                console.log('any')
                            }
                            resolve(res)


                        }).catch((er) => {
                            handleErrorMessage(er, notifications.error)
                            reject(er)
                        })
                    }
                })

            else {
                axiosIns.delete(url, { ...params, ...config }).then((res) => {

                    if (res.status == 200 && typeof (notifications.success) == 'string') {
                        console.log('any')
                    }
                    resolve(res)


                }).catch((er) => {
                    handleErrorMessage(er, notifications.error)
                    reject(er)
                })
            }
        })
    };


    return {
        GET,
        POST,
        DELETE,
    }
}