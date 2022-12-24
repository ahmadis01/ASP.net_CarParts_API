import { configureStore } from "@reduxjs/toolkit";
import { brandReducer } from "./brands";
import CountrySlice from './countries/'
export const store = configureStore({
    reducer: {
        brand: brandReducer,
        country: CountrySlice
    }
});
export type RootState = ReturnType<typeof store.getState>
// Inferred type: {posts: PostsState, comments: CommentsState, users: UsersState}
export type AppDispatch = typeof store.dispatch