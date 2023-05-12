import { GetAllParts } from "@/api/Part/GetAllDto";
import { createSlice, PayloadAction } from "@reduxjs/toolkit";

interface State {
    parts: GetAllParts[]
}
const initialState: State = {
    parts: []
}

const PartSlice = createSlice({
    name: 'part',
    initialState,
    reducers: {
        setParts(state: State, action: PayloadAction<GetAllParts[]>) {
            state.parts = action.payload
        }
    }
})

export default PartSlice.reducer
export const partActions = PartSlice.actions;