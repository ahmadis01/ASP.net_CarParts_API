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
import { Autocomplete, MenuItem, Select } from '@mui/material';
import { useDispatch, useSelector } from 'react-redux';
import { AppDispatch, RootState } from '@/store';
import { BrandItem } from '@/api/Brand/dto';
import { useState, useEffect } from 'react'
import { fetchBrands } from '@/store/brands';
import Upload from '../Upload';
import { addCar } from '@/store/cars';

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

            dispatch(addCar(values))

        },

    })

    const dispatch = useDispatch<AppDispatch>()

    const brands = useSelector<RootState, BrandItem[]>((state) => state.brand.brands)



    const [open, setOpen] = useState(false);

    useEffect(() => {
        dispatch(fetchBrands());
    }, [])
    const handleClickOpen = () => {
        setOpen(true);
    };

    const handleClose = () => {
        setOpen(false);
    };

    const submit = () => {
        // carForm.validateForm().then((result,e) => {
        //     console.log(result);

        // })


    }

    return (
        <div>

            <Button variant="contained" onClick={handleClickOpen}  >
                إضافة سيارة
            </Button>
            <Dialog open={open} onClose={handleClose}>

                <form onSubmit={carForm.handleSubmit} >
                    <DialogTitle>إضافة سيارة</DialogTitle>
                    <DialogContent className='flex py-4 flex-col min-w-[500px] space-y-10'>

                        <Autocomplete
                            disablePortal
                            options={brands.map(b => ({ title: b.name, id: b.id }))}
                            getOptionLabel={(o) => o.title || ''}
                            onChange={(e, value) => { carForm.setFieldValue('brandId', value?.id) }}
                            renderInput={(params) => <TextField {...params} label="ماركة السيارة"
                                value={carForm.values.brandId}

                            />}
                        />

                        <TextField name='name' id='name' label='اسم السيارة' value={carForm.values.name} onChange={carForm.handleChange} error={carForm.touched.name} />
                        <TextField name='model' id='name' helperText='سنة التصنيع' label='موديل السيارة' value={carForm.values.model} onChange={carForm.handleChange} error={carForm.touched.model} />

                        <div>

                            <Upload label="صورة السيارة" name='car image' onChange={(e: any) => {
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
        </div>
    );
}
