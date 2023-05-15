import { axiosIns } from "@/libs/axios";
import { AxiosResponse } from "axios";
import { AddPartDTO } from "./AddPartDto";
import { serialize } from "object-to-formdata";
import { GetAllParts, GetAllPartsParams } from "./GetAllDto";

export enum PartsEndpoints {
    base = "/part"
}

export class PartApi {
    static async getParts(params: Partial< GetAllPartsParams>) {
        console.log('GetParts', params)
        try {
            const { data } = await axiosIns.get<GetAllParts>(PartsEndpoints.base, { params })
            return data;
        }
        catch (er) {
            throw (er)
        }
    }

    
    static async addPart(dto: AddPartDTO) {
        try {
            const { data } = await axiosIns.post<string, AxiosResponse<boolean>>(PartsEndpoints.base, serialize(dto, {
                dotsForObjectNotation:true,
                allowEmptyArrays:true,
            }))
            return data
        }
        catch (er) {
            throw (er)
        }
    }
}


