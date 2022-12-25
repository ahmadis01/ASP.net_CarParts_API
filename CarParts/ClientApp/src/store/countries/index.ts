import { AnyAction, AsyncThunkAction, createAsyncThunk, createSlice, PayloadAction } from '@reduxjs/toolkit';
import { CountryItem } from '~/api/Brand/dto';
import { axiosIns } from '../../libs/axios';
import { CountryController } from '~/api/Country';


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