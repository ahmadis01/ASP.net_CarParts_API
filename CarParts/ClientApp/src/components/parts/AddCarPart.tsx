import { BrandItem } from '@/api/Brand/dto'
import { GetAllCar } from '@/api/Car/dto'
import { CarPartApi } from '@/api/CarPart'
import { AddCarPartDto } from '@/api/CarPart/dto.ts'
import { InventoryItem } from '@/api/Inventory/dto'
import { GetPartsDTO } from '@/api/Part/dto'
import { RootState } from '@/store'
import { Add, CheckBox, Close } from '@mui/icons-material'
import { Button, Dialog, DialogContent, DialogTitle, FormControl, FormLabel, IconButton, InputLabel, MenuItem, Modal, Select, TextField } from '@mui/material'
import { Box } from '@mui/system'
import { useState } from 'react'
import { Controller, useForm } from 'react-hook-form'
import { useMutation } from 'react-query'
import { useSelector } from 'react-redux'
interface PropsType {
    cars: GetAllCar[],
    brands: BrandItem[],
    inventories: InventoryItem[],
}
export default function AddPart(props: PropsType) {
    const { control, } = useForm<AddCarPartDto>({
        defaultValues: { ... new AddCarPartDto() },

    })
    const parts = useSelector<RootState, GetPartsDTO[]>(s => s.part.parts)

    const [open, setOpen] = useState(false)


    const mutation = useMutation('carPart', {
        mutationFn: CarPartApi.AddCarPart,
        onSuccess: () => console.log('test')

    })

    const onSubmit = () => mutation.mutate()

    return (
        <>
            <Button onClick={() => setOpen(true)} variant='contained'>
                إضافة قطعة لسيارة
                <Add />
            </Button>
            <Dialog maxWidth='xl' open={open}>
                <form>

                    <Box display={'flex'} paddingRight={2} justifyContent={'space-between'} alignItems={'center'}>

                        <DialogTitle>إضافة قطعة</DialogTitle>
                        <IconButton onClick={() => setOpen(false)}><Close /></IconButton>
                    </Box>

                    <DialogContent sx={{ minWidth: '700px' }} >

                        <Box className='grid grid-cols-2' paddingY={2} gap={2}>
                            <Controller rules={{ required: ' يرجى اختيار السيارات المرتبطة بالقطعة' }} name='carId' control={control} render={({ field, fieldState }) =>
                                <FormControl  >
                                    <InputLabel id='carType'>السيارات المرتبطة بالقطعة</InputLabel>
                                    <Select {...field} multiple label='السيارات المرتبطة بالقطعة' labelId='carType'>

                                        {
                                            props.cars.map(c => <MenuItem value={c.id} key={c.id}>{c.name}</MenuItem>)
                                        }

                                    </Select>
                                </FormControl>
                            } />



                            <Controller rules={{ required: 'يرجى اختيار نوع القطعة' }} name='partId' control={control} render={({ field, fieldState }) =>


                                <FormControl >
                                    <InputLabel id='carType'>نوع القطعة</InputLabel>
                                    <Select {...field} label='نوع القطعة' labelId='carType'>

                                        {
                                            parts.map(p => <MenuItem key={p.id}>{p.name}</MenuItem>)
                                        }

                                    </Select>
                                </FormControl>
                            } />

                            <FormControl >
                                <InputLabel id='carType'>الشركة المصنعة</InputLabel>
                                <Select label='الشركة المصنعة' labelId='carType'>

                                    {
                                        props.brands.map(b => <MenuItem key={b.id}>{b.name}</MenuItem>)
                                    }

                                </Select>
                            </FormControl>








                            <FormControl >
                                <InputLabel id='carType'>إضافة للمستودع</InputLabel>
                                <Select label='إضافة للمستودع' labelId='carType'>

                                    {
                                        parts.map(p => <MenuItem key={p.id}>{p.name}</MenuItem>)
                                    }

                                </Select>
                            </FormControl>




                            <TextField label='السعر الأصلي' type='number'></TextField>

                            <TextField label='سعر المبيع' type='number'></TextField>




                            <Button className='col-span-2 ' sx={{ borderRadius: 2.5, padding: 1.5 }} variant='contained' >حفظ القطعة</Button>
                        </Box>

                    </DialogContent>

                </form>
            </Dialog>



        </>
    )
}
