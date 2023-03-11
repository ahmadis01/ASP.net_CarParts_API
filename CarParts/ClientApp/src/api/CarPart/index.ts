import { axiosIns } from "@/libs/axios";
import { AxiosResponse, isAxiosError } from "axios";
import { AddCarPartDto } from "./dto.ts";
export enum CAR_PART_API {
    base = '/api/CarPart'
}


export class CarPartApi {
    static AddCarPart = async () => {
        try {
            const { data } = await axiosIns.post<AddCarPartDto, AxiosResponse<boolean>>(CAR_PART_API.base)
            return data
        }
        catch (er) {
            if (isAxiosError(er))
                console.log(er.response)
            throw er
        }
    }
}