import { CustomerItem } from '@/api/Customer/GetAll'
import { AddInvoiceDto } from '@/api/Invoice/AddInvoiceDto'
import { GetAllParts, PartItem } from '@/api/Part/GetAllDto'
import { Button, Dialog, DialogActions, DialogContent, DialogTitle, FormControl, FormHelperText, InputLabel, MenuItem, Modal, Select, Table, TableBody, TableCell, TableHead, TableRow, TextField } from '@mui/material'
import React, { useEffect } from 'react'
import { Controller, useForm } from 'react-hook-form'



interface Props {
    is: boolean,
    onClose: (is: boolean) => void,
    customers: CustomerItem[],
    parts: PartItem[]
}



export default ((props: Props) => {

    useEffect(() => {
        console.log('set parts ', props.parts)
        if (props.parts)
            setValue('parts', props.parts.map((part) => ({
                partId: part.id,
                price: part.sellingPrice,
                quantity: 1,
                storeId: 0,

            })))


    }, [props.parts])

    const initialFormState: AddInvoiceDto = {
        clientId: 0,
        coast: 0,
        date: new Date().toISOString().substring(0, 10),
        isImport: false,
        notes: '',
        parts: [],
        services: 0,
    }
    const { handleSubmit, control, setValue, reset, getValues } = useForm<AddInvoiceDto>({
        defaultValues: { ...initialFormState }
    })


    const onSubmit = () => { }





    return (
        <div>

            <Dialog open={props.is} maxWidth={'md'} fullWidth >
                <DialogTitle>
                    إنشاء فاتورة
                </DialogTitle>
                <DialogContent>
                    <form onSubmit={handleSubmit(onSubmit)}>
                        <div className="grid space-y-8 ">

                            {
                                props.parts.length &&

                                <Table>
                                    <TableHead>
                                        <TableRow>
                                            <TableCell width={200}>اسم القطعة</TableCell>
                                            <TableCell>الكمية</TableCell>
                                            <TableCell>السعر</TableCell>
                                            <TableCell>السعر الإجمالي</TableCell>
                                            <TableCell>المستودع</TableCell>
                                        </TableRow>
                                    </TableHead>
                                    <TableBody className='bg-gray-50'>

                                        {


                                            props.parts.map(part => {
                                                return (
                                                    <TableRow key={part.id}>
                                                        <TableCell>{part.name}</TableCell>
                                                        <TableCell><TextField size='small' type='number' value={1}></TextField></TableCell>
                                                        <TableCell> <TextField size='small' type='number' value={part.sellingPrice}></TextField></TableCell>
                                                        <TableCell> <TextField size='small' type='number' value={part.sellingPrice}></TextField></TableCell>
                                                        <TableCell>السليمانية</TableCell>
                                                    </TableRow>
                                                )
                                            })
                                        }
                                    </TableBody>
                                </Table>


                            }

                            <Controller name='clientId' control={control} render={({ field, fieldState }) => {
                                return <FormControl className='py-4 my-5 ' sx={{ marginTop: '10px' }} error={!!fieldState.error}>
                                    <InputLabel id="brand-id-label">الزبون</InputLabel>
                                    <Select
                                        {...field}
                                        name='brandId'
                                        labelId="brand-id-label"
                                        label="الزبون"
                                    >
                                        {
                                            props.customers.map((c) => <MenuItem key={c.id} value={c.id}>{c.name}</MenuItem>)
                                        }

                                    </Select>
                                    <FormHelperText>
                                        {fieldState.error?.message}
                                    </FormHelperText>
                                </FormControl>
                            }} />

                            <Controller name='date' control={control} render={({ field, fieldState }) => {
                                return <TextField label='تاريخ الفاتورة' type={'date'} inputProps={{ max: new Date().toISOString().substring(0, 10), className: 'text-right' }} {...field}></TextField>
                            }} />





                        </div>
                    </form>
                </DialogContent>
                <DialogActions>
                    <Button>حفظ</Button>
                    <Button onClick={() => props.onClose(false)}>اغلاق</Button>
                </DialogActions>
            </Dialog>

        </div >
    )
})
