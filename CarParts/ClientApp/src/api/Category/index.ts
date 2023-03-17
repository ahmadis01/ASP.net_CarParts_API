import { axiosIns } from "@/libs/axios";
import { CategoryItem } from "./dto";

export class CategoryApi {
    static async GetAll() {
        try {
            const { data } = await axiosIns.get<CategoryItem[]>('/Category')
            return data
        }
        catch (er) {
            console.log(er)
            throw er
        }
    }
}