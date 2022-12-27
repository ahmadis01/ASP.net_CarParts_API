import React, { useState } from 'react'
import { carState } from '../store/cars'
import { useSelector } from 'react-redux'
import { RootState } from '../store'
import AddCar from '@/components/cars/AddCar'
export default function Cars() {
    const cars = useSelector<RootState>(state => state.brand.brands)
    return (
        <div>
            <AddCar></AddCar>
        </div>
    )
}
