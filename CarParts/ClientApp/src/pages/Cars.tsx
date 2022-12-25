import React, { useState } from 'react'
import { carState } from '../store/cars'
import { useSelector } from 'react-redux'
import { RootState } from '../store'

export default function Cars() {
    const cars = useSelector<RootState>(state => state.brand.brands)
    return (
        <div>Cars</div>
    )
}
