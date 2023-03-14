import { axiosIns } from "@/libs/axios"

export enum COUNTRY_API {
    Base = 'Country'
}

export class CountryApi {
    static fetchCountries = async () => {
        try {
            const { data } = await axiosIns.get(COUNTRY_API.Base)
            return data
        }
        catch (er) {
            throw (er)
        }
    }
}