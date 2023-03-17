import { axiosIns } from "@/libs/axios"
import { InventoryItem } from "./dto"

export enum INVENTORY_API {
    Base = '/Store'
}


export class InventoryApi {
    static async GetAll() {
        try {
            const { data } = await axiosIns.get<InventoryItem[]>(INVENTORY_API.Base)
            return data
        }
        catch (er) {
            console.log(er)
            throw er
        }
    }
}