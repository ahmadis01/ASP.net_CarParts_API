import { createAsyncThunk, createSlice, AnyAction } from '@reduxjs/toolkit';
import { axiosIns } from '@/libs/axios';
import { BrandController } from '@/api/Brand';
import { BrandItem } from '@/api/Brand/dto';
export type BrandsState = {
    brands: BrandItem[]
}
const initialState: BrandsState = {
    brands: [],

}

// ASYNC THUNKS

export const fetchBrands = createAsyncThunk('brand/get', async () => {
    try {
        const res = await axiosIns.get(BrandController.BASE);
        return res.data
    }

    catch (er) {
        throw er
    }

})


const brandSlice = createSlice({
    name: "brand",
    initialState,
    reducers: {


    },

    extraReducers(builder) {
        builder.addCase(fetchBrands.fulfilled, (state, action: AnyAction) => {
            state.brands = action.payload
        })
    },


})




export const BrandsActions = brandSlice.actions;
export const brandReducer = brandSlice.reducer;