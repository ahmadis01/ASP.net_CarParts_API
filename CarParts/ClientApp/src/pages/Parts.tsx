import PartsTable from '@/components/parts/PartsTable'
import { PartApi } from '@/api/Part'
import { useQuery } from 'react-query'
import { Card, Typography } from '@mui/material'
import AddCarPart from '@/components/parts/AddCarPart'
import { useDispatch, useSelector } from 'react-redux'
import { AppDispatch, RootState } from '@/store'
import { partActions } from '@/store/parts'
import { CarApi } from '@/api/Car'
import { useState } from 'react'
import { GetAllCar } from '@/api/Car/dto'
import { BrandItem } from '@/api/Brand/dto'

function Products() {
    const [carsList, setCarsList] = useState<GetAllCar[]>([])
    const brands = useSelector<RootState, BrandItem[]>(s => s.brand.brands)
    const dispatch = useDispatch<AppDispatch>()

    const partQuery = useQuery('part', PartApi.getParts, {
        onSuccess: (data) => {
            dispatch(partActions.setParts(data))
        }
    })

    const carQuery = useQuery('car', CarApi.fetchCars, {
        onSuccess: (cars => setCarsList(cars))
    })

    return (
        <div>
            <Card className='mb-2 p-4 flex justify-between items-center'>
                <Typography variant='h2' fontSize={24}>قطع السيارات</Typography>
                <AddCarPart brands={brands} inventories={[]} cars={carsList}></AddCarPart>
            </Card>
            <PartsTable />
        </div>
    )
}

export default Products