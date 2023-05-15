import { FormControl, InputLabel, MenuItem, Select } from '@mui/material'
import { DesktopDatePicker } from '@mui/x-date-pickers'
import React, { Component, useState } from 'react'
import { GetAllPartsParams } from '@/api/Part/GetAllDto'
import { GetAllCar } from '@/api/Car/dto'
import { BrandItem } from '@/api/Brand/dto'
import { CategoryItem } from '@/api/Category/dto'
import { InventoryItem } from '@/api/Inventory/dto'
import { CountryItem } from '@/api/Country/dto'


interface Props {
    carsList: GetAllCar[]
    brandsList: BrandItem[]
    categoriesList: CategoryItem[]
    inventoriesList: InventoryItem[]
    countriesList: CountryItem[],
    params: GetAllPartsParams,
    onFilterChange: (key: string, value: string | null) => void
}

export default ({
    carsList,
    brandsList,
    categoriesList,
    countriesList,
    inventoriesList,
    params,
    onFilterChange,

}: Props) => {

    return (
        <div>

            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
                <FormControl size='small'>
                    <InputLabel id='carCategory' >تصنيف القطعة</InputLabel>
                    <Select onChange={(e) => onFilterChange(e.target.name, e.target.value)} name={'OrderBy' as keyof GetAllPartsParams} value={params.OrderBy ?? ''} label='تصنيف القطعة' labelId='carCategory'>

                        {
                            categoriesList.map(p => <MenuItem value={p.id} key={p.id}>{p.name}</MenuItem>)
                        }

                    </Select>
                </FormControl>
                <FormControl size='small'   >
                    <InputLabel id='carCategory'>السيارة التابعة للقطعة</InputLabel>
                    <Select onChange={(e) => onFilterChange(e.target.name, e.target.value)} name={'CarId' as keyof GetAllPartsParams} value={params.CarId ?? ''} label='السيارة التابعة للقطعة' labelId='carCategory'>

                        {
                            carsList.map(p => <MenuItem value={p.id} key={p.id}>{p.name}</MenuItem>)
                        }

                    </Select>
                </FormControl>


                <FormControl size='small'   >
                    <InputLabel id='carCategory'>العلامة التجارية</InputLabel>
                    <Select onChange={(e) => onFilterChange(e.target.name, e.target.value)} name={'BrandId' as keyof GetAllPartsParams} value={params.BrandId ?? ''} label='العلامة التجارية' labelId='carCategory'>

                        {
                            brandsList.map(p => <MenuItem value={p.id} key={p.id}>{p.name}</MenuItem>)
                        }

                    </Select>
                </FormControl>


                <FormControl size='small'   >
                    <InputLabel id='carCategory'>بلد المنشأ</InputLabel>
                    <Select onChange={(e) => onFilterChange(e.target.name, e.target.value)} name={'CountryId' as keyof GetAllPartsParams} value={params.CountryId ?? ''} label='بلد المنشأ' labelId='carCategory'>

                        {
                            countriesList.map(p => <MenuItem value={p.id} key={p.id}>{p.name}</MenuItem>)
                        }

                    </Select>
                </FormControl>
                <FormControl   >
                    <DesktopDatePicker
                        label="تاريخ الإضافة"
                        format="MM/DD/YYYY"
                        value={params.Date}
                        slotProps={{ textField: { size: 'small' } }}

                    />
                </FormControl>

                <FormControl size='small'   >
                    <InputLabel id='carCategory'>المستودع - المتجر</InputLabel>
                    <Select onChange={(e) => onFilterChange(e.target.name, e.target.value)} name={'StoreId' as keyof GetAllPartsParams} value={params.StoreId ?? ''} label='المستودع - المتجر' labelId='carCategory'>

                        {
                            inventoriesList.map(p => <MenuItem value={p.id} key={p.id}>{p.location}</MenuItem>)
                        }

                    </Select>
                </FormControl>


            </div>

        </div >
    )
}
