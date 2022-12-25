import { AnyAction, PayloadAction, createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { axiosIns } from "../../libs/axios";
import { CarAPI } from "~/api/Car";


const fetchCars = createAsyncThunk('car/fetch', async () => {
    try {
        const { data } = await axiosIns.get(CarAPI.base)
        return data;
    }

    catch (er) {
        throw (er)
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
        })
    },
})


