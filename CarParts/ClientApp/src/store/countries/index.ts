import { AnyAction, AsyncThunkAction, createAsyncThunk, createSlice, PayloadAction } from '@reduxjs/toolkit';
import { CountryItem } from './dto';
import axios from 'axios';
import { axiosIns } from '../../libs/axios';
import { CountryController } from '../../api/endpoints/country';


interface CountryState {
    countries: Array<CountryItem>
}
const initialState: CountryState = {
    countries: []
}

export const countriesSlice = createSlice({
    name: 'countries',
    initialState,
    reducers: {

        
    },
    extraReducers(builder) {
        builder.addCase(fetchCountries.fulfilled, (state, action: any) => {
            state.countries = [...action.payload]
        })
    },


})


// React Thunk Actions

export const fetchCountries = createAsyncThunk('countries/fetchCountries', async () => {
    try {

        const { data } = await axiosIns.get<CountryItem[]>(CountryController.Base)
        return [...data]
    }
    catch (er) {
        console.log(er);

    }
})





export default countriesSlice.reducer