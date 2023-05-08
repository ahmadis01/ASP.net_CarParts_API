import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogTitle from '@mui/material/DialogTitle';
import { AddCarDTO, GetAllCar } from '@/api/Car/dto';
import { FormHelperText, IconButton, InputLabel, MenuItem, Select } from '@mui/material';
import { useDispatch, useSelector } from 'react-redux';
import { AppDispatch, RootState } from '@/store';
import { BrandItem } from '@/api/Brand/dto';
import { useState, useEffect } from 'react'
import Upload from '../Upload';
import Box from '@mui/material/Box';
import FormControl from '@mui/material/FormControl';
import { Add, Close } from '@mui/icons-material';
import { CarActions } from '@/store/cars';
import { useMutation, useQueryClient } from 'react-query';
import { CarApi } from '@/api/Car';
import { Controller, SubmitHandler, useForm } from 'react-hook-form'
import { toast } from 'react-toastify';


interface propsType {
    carModifyDto: GetAllCar | null,
    onCloseDialog: () => void
}

export default function FormDialog({ carModifyDto, onCloseDialog }: propsType) {
    const initialFormState = {
        brandId: '',
        carCategoryId: '',
        image: null,
        model: '',
        name: '',
    }
    const queryClient = useQueryClient()
    const showModal = useSelector<RootState, boolean>((state) => state.car.carFormModal);
    const brands = useSelector<RootState, BrandItem[]>((state) => state.brand.brands)
    const dispatch = useDispatch<AppDispatch>()
    const [imageUrl, setImageUrl] = useState('')
    const { handleSubmit, control, setValue, reset, } = useForm<Omit<AddCarDTO, 'imageUrl'>>({
        defaultValues: { ...initialFormState }
    })

    const resetForm = () => {
        reset({ ...initialFormState });
        setImageUrl('')
        dispatch(CarActions.setCarModal(false));

    }

    const mutation = useMutation(CarApi.addCar, {

        onSuccess: (d: GetAllCar) => {
            toast(`تمت إضافة ${d.name}`, {
                theme: 'light',
                type: 'success'
            })
            queryClient.invalidateQueries('car');
            resetForm()
        }
    })

    const deleteCar = useMutation(CarApi.deleteCar, {
        onSuccess: () => {
            queryClient.invalidateQueries('car')
            resetForm()
        }
    });

    const onsubmit: SubmitHandler<Omit<AddCarDTO, 'imageUrl'>> = (data) => {
        mutation.mutate(data)
    }

    useEffect(() => {
        if (carModifyDto !== null && carModifyDto.id) {
            dispatch(CarActions.setCarModal(true));
            reset({
                brandId: carModifyDto.brandId,
                carCategoryId: carModifyDto.carCategoryId,
                model: carModifyDto.model,
                name: carModifyDto.name,
                image: null,
            })
            setImageUrl(carModifyDto.image)

        }
    }, [carModifyDto])

    useEffect(() => {
        if (showModal === false) {
            resetForm();
            onCloseDialog();
        }
    }, [showModal])


    return (
        <div>

            <Button variant="text" onClick={() => dispatch(CarActions.setCarModal(true))}  >
                إضافة سيارة
                <Add></Add>
            </Button>

            <Dialog open={showModal} >
                <Box>


                    <form onSubmit={handleSubmit(onsubmit)} className='overflow-hidden'>
                        <div className="flex justify-between items-center pl-4 ">
                            <DialogTitle
                            >إضافة سيارة

                            </DialogTitle>


                            <IconButton onClick={() => dispatch(CarActions.setCarModal(false))}><Close /></IconButton>
                        </div>
                        <DialogContent className='flex flex-col min-w-[35rem] p-2 gap-4 '>



                            <Controller rules={{ required: 'يرجى اختيار الشركة المصنعة' }} name='brandId' control={control} render={({ field, fieldState }) =>
                                <FormControl className='py-4 my-5 ' sx={{ marginTop: '10px' }} error={!!fieldState.error}>
                                    <InputLabel id="brand-id-label">الشركة المصنعة</InputLabel>
                                    <Select
                                        {...field}
                                        name='brandId'
                                        labelId="brand-id-label"
                                        label="الشركة المصنعة"
                                    >
                                        {
                                            brands.map((b) => <MenuItem key={b.id} value={b.id}>{b.name}</MenuItem>)
                                        }

                                    </Select>
                                    <FormHelperText>
                                        {fieldState.error?.message}
                                    </FormHelperText>
                                </FormControl>
                            } />


                            <Controller rules={{ required: 'هذا الحقل مطلوب' }} name='name' control={control} render={({ field, fieldState }) =>
                                <TextField error={!!fieldState.error}
                                    helperText={fieldState.error?.message}
                                    {...field} name='name' id='car-name' label='اسم السيارة'
                                />
                            }
                            />

                            <Controller rules={{ required: 'موديل السيارة مطلوب' }} name='model' control={control} render={({ field, fieldState }) =>

                                <TextField error={!!fieldState.error}   {...field} name='model' id='car-model' helperText={fieldState.error?.message} label='موديل السيارة' />
                            }
                            />
                            <div>

                                <Upload url={imageUrl} onChange={({ file, src }) => { setValue('image', file), setImageUrl(src) }} label="صورة السيارة" name='image' />

                            </div>

                        </DialogContent>
                        <DialogActions sx={{ justifyContent: 'space-between' }}>

                            <Box gap={2} display='flex' >
                                <Button variant='contained' type='submit'>إضافة السيارة</Button>
                                <Button onClick={() => dispatch(CarActions.setCarModal(false))}>الغاء</Button>

                            </Box >
                            {
                                carModifyDto && <Button variant='text' color='error' onClick={() => deleteCar.mutate(carModifyDto.id)}>حذف السيارة</Button>
                            }

                        </DialogActions>
                    </form>
                </Box>

            </Dialog>
        </div >
    );
}
