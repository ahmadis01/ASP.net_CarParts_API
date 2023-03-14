import { axiosIns } from "@/libs/axios";
import { AxiosResponse } from "axios";
import { AddPartDTO, GetPartsDTO } from "./dto";
export enum PartsEndpoints {
    base = "/part"
}

export class PartApi {
    static async getParts() {
        try {
            const { data } = await axiosIns.get<GetPartsDTO[]>(PartsEndpoints.base)
            return data;
        }
        catch (er) {
            throw (er)
        }
    }

    static async addPart(dto: AddPartDTO) {
        try {

            const { data } = await axiosIns.post<string, AxiosResponse<boolean>>(PartsEndpoints.base, dto)
            return data
        }
        catch (er) {
            throw (er)
        }
    }
}

