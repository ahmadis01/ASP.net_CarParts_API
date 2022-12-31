import * as React from 'react';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogTitle from '@mui/material/DialogTitle';
import { useFormik } from 'formik';
import * as yup from 'yup';
import { AddCarDTO } from '@/api/Car/dto';
import { Autocomplete, InputLabel, MenuItem, Select } from '@mui/material';
import { useDispatch, useSelector } from 'react-redux';
import { AppDispatch, RootState } from '@/store';
import { BrandItem } from '@/api/Brand/dto';
import { useState, useEffect } from 'react'
import Upload from '../Upload';
import Box from '@mui/material/Box';
import FormControl from '@mui/material/FormControl';
import { Add } from '@mui/icons-material';


const validationSchema = yup.object({
    name: yup.string().required('اسم السيارة مطلوب'),
    brandId: yup.string().required('ماركة السيارة مطلوبة'),
    model: yup.number().required().min(1970),

})

export default function FormDialog() {

    const carForm = useFormik({
        initialValues: {
            ...new AddCarDTO()
        },
        validationSchema,
        onSubmit: (values) => {
            console.log('submited');

            // dispatch(addCar(values))

        },

    })


    const brands = useSelector<RootState, BrandItem[]>((state) => state.brand.brands)

    const [open, setOpen] = useState(false);




    const handleClickOpen = () => {
        setOpen(true);
    };

    const handleClose = () => {
        setOpen(false);
    };



    const [age, setAge] = React.useState('');

    const handleChange = (event: any) => {
        setAge(event.target.value as string);
    };
    return (
        <div>

            <Button variant="text" onClick={handleClickOpen}  >
                إضافة سيارة
                <Add></Add>
            </Button>
            <Dialog open={open} onClose={handleClose}>

                <form onSubmit={carForm.handleSubmit} >
                    <DialogTitle>إضافة سيارة</DialogTitle>
                    <DialogContent className='flex flex-col min-w-[31.25rem] p-2 gap-4 '>

                        <FormControl className='py-4 my-5 ' sx={{ marginTop: '10px' }} >
                            <InputLabel id="brand-id-label">الشركة المصنعة</InputLabel>
                            <Select

                                value={carForm.values.brandId}
                                onChange={(e) => carForm.setFieldValue('brandId', e.target.value)}
                                labelId="brand-id-label"
                                label="الشركة المصنعة"
                            >
                                {
                                    brands.map((b) => <MenuItem key={b.id} value={b.id}>{b.name}</MenuItem>)
                                }
                            </Select>
                        </FormControl>
                        <TextField name='name' id='car-name' label='اسم السيارة' value={carForm.values.name} onChange={carForm.handleChange} error={carForm.touched.name} helperText={carForm.touched.name && carForm.errors.name}
                        />
                        <TextField name='model' id='car-model' helperText='سنة التصنيع' label='موديل السيارة' value={carForm.values.model} onChange={carForm.handleChange} error={carForm.touched.model} />

                        <div>

                            <Upload label="صورة السيارة" name='image' onChange={(e: any) => {
                                carForm.setFieldValue('image', e.file)
                            }} />
                        </div>

                    </DialogContent>
                    <DialogActions>
                        <Button onClick={handleClose}>الغاء</Button>
                        <Button type='submit'>إضافة السيارة</Button>
                    </DialogActions>
                </form>
            </Dialog>
        </div >
    );
}
