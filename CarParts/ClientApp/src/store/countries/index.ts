import { ActionCreatorWithPayload, AnyAction, AsyncThunkAction, createAsyncThunk, createSlice, PayloadAction } from '@reduxjs/toolkit';
import { CountryItem } from '~/api/Country/dto';


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
        SetCountries: (state: CountryState, action: PayloadAction<CountryItem[]>) => {
            state.countries = action.payload
        }
    },
})
export default countriesSlice.reducer

export const CountryActions = countriesSlice.actions