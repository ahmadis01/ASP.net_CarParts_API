import { axiosIns } from "@/libs/axios";

enum InvoiceEndPoints {
    Base = '/Invoice'
}


export class InvoiceApi {
    static async GetAll() {
        return await axiosIns.get<any[]>(InvoiceEndPoints.Base)
    }
}