import React, { useState } from 'react'
import { axiosIns } from '@/libs/axios'
import { QueryClient, QueryClientProvider, useQuery } from 'react-query'
import { InventoryAPI } from '@/api/Inventory'


function Inventories() {
    const [inventories, setInventories] = useState([])
    const { isLoading, error, data } = useQuery('inventory', () => axiosIns.get(InventoryAPI.Base), {
        onSuccess: (res) => {
            setInventories(res?.data)
        }
    })


    return (

        <div>Inventories  : {JSON.stringify(inventories)} is Loading : {isLoading}</div>
    )
}

export default Inventories