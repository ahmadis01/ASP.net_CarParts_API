import { axiosIns } from "@/libs/axios";
import { AddCarDTO, GetAllCar } from "../Car/dto";
import { serialize } from "object-to-formdata";

export enum CAR_API {
    base = 'Car'
}


export class CarApi {
    static fetchCars = async () => {
        try {
            const { data } = await axiosIns.get<GetAllCar[]>(CAR_API.base)
            return data;
        }
        catch (er) {
            throw (er)
        }
    }

    static addCar = async (payload: Omit<AddCarDTO, 'imageUrl'>) => {
        try {

            const { data } = await axiosIns.post(CAR_API.base, serialize(payload))
            return data
        }

        catch (er) {
            throw (er)
        }

    }




    static deleteCar = async (carId: string | number) => {
        try {
            const res = await axiosIns.delete(`${CAR_API.base}/${carId}`,)
            return res
        }

        catch (er) {
            throw (er)
        }
    }


}

