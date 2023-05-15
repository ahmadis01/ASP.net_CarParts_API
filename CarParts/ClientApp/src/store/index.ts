import { configureStore } from "@reduxjs/toolkit";
import { BrandReducer } from "./brands";
import CountrySlice from './countries/'
import CarSlice from './cars'
import PartSlice from "./parts";
export const store = configureStore({
    reducer: {
        brand: BrandReducer,
        country: CountrySlice,
        car: CarSlice,
        part: PartSlice
    }
});


export type RootState = ReturnType<typeof store.getState>
// Inferred type: {posts: PostsState, comments: CommentsState, users: UsersState}
export type AppDispatch = typeof store.dispatch