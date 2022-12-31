import { configureStore } from "@reduxjs/toolkit";
import { brandReducer } from "./brands";
import CountrySlice from './countries/'
import CarSlice from './cars'
export const store = configureStore({
    reducer: {
        brand: brandReducer,
        country: CountrySlice,
        car: CarSlice
    }
});
export type RootState = ReturnType<typeof store.getState>
// Inferred type: {posts: PostsState, comments: CommentsState, users: UsersState}
export type AppDispatch = typeof store.dispatch