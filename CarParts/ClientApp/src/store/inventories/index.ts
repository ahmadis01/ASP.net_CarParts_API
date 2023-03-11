import { InventoryItem } from "@/api/Inventory/dto";
import { PayloadAction, createSlice } from "@reduxjs/toolkit";

interface inventoryState {
    inventories: InventoryItem[]
}

const initialState: inventoryState = {
    inventories: []
}

const inventorySlice = createSlice({
    name: 'inventory',
    initialState,
    reducers: {
        setinventories: (state: inventoryState, action: PayloadAction<InventoryItem[]>) => { state.inventories = action.payload }
    }
})

export default inventorySlice;

export const inventoryActions = inventorySlice.actions; 