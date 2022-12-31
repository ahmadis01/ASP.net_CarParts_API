import { AnyAction, PayloadAction, createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { axiosIns } from "../../libs/axios";
import { CarAPI } from "~/api/Car";
import { serialize } from "object-to-formdata";

export const fetchCars = createAsyncThunk('car/fetch', async () => {
    try {
        const { data } = await axiosIns.get(CarAPI.base)
        return data;
    }

    catch (er) {
        throw (er)
    }
})

export const addCar = createAsyncThunk('car/add', async (payload: any) => {
    console.log(payload);
    try {
        const formData = serialize(payload)
        const response = await axiosIns.post(CarAPI.base, formData)

        console.log(response);

    }

    catch (er) {
        console.log(er);
    }

})

export interface carState {
    cars: Array<any>
}

const initialState: carState = {
    cars: []
}

const carSlice = createSlice({
    name: 'car',
    initialState,

    reducers: {},

    extraReducers(builder) {
        builder.addCase(fetchCars.fulfilled, (state, action: PayloadAction<any[]>) => {
            if (action.payload)
                state.cars = [...action.payload]
        }).addCase(addCar.fulfilled, (state, action: PayloadAction<any>) => {
            state.cars.push(action.payload)
        })
    },
})

export default carSlice.reducer


