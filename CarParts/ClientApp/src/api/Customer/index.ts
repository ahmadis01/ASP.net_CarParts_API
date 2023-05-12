import { axiosIns } from "@/libs/axios";
import { CustomerItem } from "./GetAll";

enum CustomerApiEndpoints {
    base = '/Client'
}


export class CustomerApi {
    static fetchCustomers = async () => {
        const response = await axiosIns.get<CustomerItem[]>(CustomerApiEndpoints.base);
        return response.data;
    }
}