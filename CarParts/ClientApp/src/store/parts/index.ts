import { GetPartsDTO } from "@/api/Part/dto";
import { createSlice, PayloadAction } from "@reduxjs/toolkit";

interface State {
    parts: GetPartsDTO[]
}
const initialState: State = {
    parts: []
}

const PartSlice = createSlice({
    name: 'part',
    initialState,
    reducers: {
        setParts(state: State, action: PayloadAction<GetPartsDTO[]>) {
            state.parts = action.payload
        }
    }
})

export default PartSlice.reducer
export const partActions = PartSlice.actions;