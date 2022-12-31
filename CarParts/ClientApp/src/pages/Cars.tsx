import React, { useEffect, useState } from 'react'
import { carState } from '../store/cars'
import { useDispatch, useSelector } from 'react-redux'
import { AppDispatch, RootState } from '../store'

import AddCar from '@/components/cars/AddCar'
import CarsList from '@/components/cars/CarsList'
import { fetchCars } from '../store/cars'
import { GetAllCar } from '@/api/Car/dto'
import { Autocomplete, Card, TextField } from '@mui/material'
import { Search } from '@mui/icons-material'
import { CountryItem } from '@/api/Country/dto'
export default function Cars() {
    const countries = useSelector<RootState, CountryItem[]>(state => state.country.countries)
    const brands = useSelector<RootState, CountryItem[]>(state => state.brand.brands)
    const dispatch = useDispatch<AppDispatch>()
    useEffect(() => {
        dispatch(fetchCars())
    }, [])
    const cars = useSelector<RootState, GetAllCar[]>(state => state.car.cars)
    return (
        <div>
            <Card className='p-4 ' elevation={0} >

                <div className="flex items-center gap-5">
                    <TextField label='ابحث عن سيارة معينة' className='w-96' />


                    <Autocomplete

                        disablePortal
                        options={countries}
                        sx={{ width: 300 }}
                        getOptionLabel={(o) => o.name}
                        renderInput={(params) => <TextField {...params} label="الدولة" />}
                    />
                    <Autocomplete
                        disablePortal
                        options={brands}
                        sx={{ width: 300 }}
                        getOptionLabel={(o) => o.name}
                        renderInput={(params) => <TextField {...params} label="الشركة المصنعة" />}
                    />

                    <AddCar></AddCar>
                </div>
            </Card>

            <div className='mt-4 px-4'>
                <CarsList carsList={cars}></CarsList>
            </div>
        </div>
    )
}
