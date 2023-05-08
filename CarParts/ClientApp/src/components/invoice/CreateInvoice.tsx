import { CustomerItem } from "@/api/Customer/GetAll";
import { AddInvoiceDto } from "@/api/Invoice/AddInvoiceDto";
import { GetAllParts, PartItem } from "@/api/Part/GetAllDto";
import {
  Button,
  Checkbox,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  FormControl,
  FormControlLabel,
  FormHelperText,
  FormLabel,
  InputLabel,
  MenuItem,
  Modal,
  Radio,
  RadioGroup,
  Select,
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableRow,
  TextField,
} from "@mui/material";
import React, { useEffect } from "react";
import { Controller, useFieldArray, useForm } from "react-hook-form";
import { z } from "zod";
interface Props {
  is: boolean;
  onClose: (is: boolean) => void;
  customers: CustomerItem[];
  parts: PartItem[];
  onSubmit: (data: AddInvoiceDto) => void;
}

export default (props: Props) => {
  useEffect(() => {
    if (props.parts)
      setValue(
        "parts",
        props.parts.map((part) => ({
          partId: part.id,
          price: part.sellingPrice,
          quantity: 1,
          storeId: 0,
        }))
      );
  }, [props.parts]);

  const initialFormState: AddInvoiceDto = {
    clientId: "",
    coast: 0,
    date: new Date().toISOString().substring(0, 10),
    isImport: false,
    notes: "",
    services: 0,
    parts: [],
  };

  const { handleSubmit, control, setValue, reset, watch, getValues } =
    useForm<AddInvoiceDto>({
      defaultValues: { ...initialFormState },
    });
  const parts = watch("parts", []); // you can also target specific fields by their names
  const service = watch("services", 0); // you can also target specific fields by their names

  const onSubmit = () => {};

  useEffect(() => {
    console.log("wtf is changing");

    setValue(
      "coast",
      parts.reduce((prev, curr) => {
        return +prev + +curr.price * +curr.quantity;
      }, 0) + +service
    );
  }, [JSON.stringify(parts), service]);
  return (
    <div>
      <Dialog open={props.is} maxWidth={"md"} fullWidth>
        <form onSubmit={handleSubmit(onSubmit)}>
          <DialogTitle>إنشاء فاتورة</DialogTitle>
          <DialogContent>
            <div className="grid grid-cols-12  gap-4">
              <Controller
                name="clientId"
                rules={{ required: true }}
                control={control}
                render={({ field, fieldState }) => {
                  return (
                    <FormControl
                      size="small"
                      className="col-span-8"
                      sx={{ mt: 1 }}
                      error={!!fieldState.error}
                    >
                      <InputLabel id="brand-id-label">الزبون</InputLabel>
                      <Select
                        {...field}
                        labelId="brand-id-label"
                        label="الزبون"
                      >
                        {props.customers.map((c) => (
                          <MenuItem key={c.id} value={c.id}>
                            {c.name}
                          </MenuItem>
                        ))}
                      </Select>
                      <FormHelperText>
                        {fieldState.error?.message}
                      </FormHelperText>
                    </FormControl>
                  );
                }}
              />

              <Controller
                name="date"
                control={control}
                render={({ field, fieldState }) => {
                  return (
                    <TextField
                      size="small"
                      sx={{ mt: 1 }}
                      className="col-span-4"
                      label="تاريخ الفاتورة"
                      type={"date"}
                      inputProps={{
                        max: new Date().toISOString().substring(0, 10),
                        className: "text-right",
                      }}
                      {...field}
                    ></TextField>
                  );
                }}
              />
              <Controller
                name="isImport"
                control={control}
                render={({ field }) => (
                  <FormControl className="col-span-12">
                    <FormLabel>نوع الفاتورة</FormLabel>
                    <RadioGroup row defaultValue={false}>
                      <FormControlLabel
                        value={false}
                        control={<Radio />}
                        label="صادرات"
                      />
                      <FormControlLabel
                        value={true}
                        control={<Radio />}
                        label="واردات"
                      />
                    </RadioGroup>
                  </FormControl>
                )}
              />

              {props.parts.length && (
                <Table className="col-span-12">
                  <TableHead>
                    <TableRow>
                      <TableCell width={200}>اسم القطعة</TableCell>
                      <TableCell>الكمية</TableCell>
                      <TableCell>السعر</TableCell>
                      <TableCell>السعر الإجمالي</TableCell>
                      <TableCell>المستودع</TableCell>
                    </TableRow>
                  </TableHead>
                  <TableBody>
                    {props.parts.map((part, i) => {
                      return (
                        <TableRow key={part.id}>
                          <TableCell>{part.name}</TableCell>
                          <TableCell>
                            <Controller
                              rules={{
                                min: {
                                  message:
                                    "يرجى إدخال كمية صحيحة (1 على الاقل )",
                                  value: 1,
                                },
                                required: "هذا الحقل مطلوب",
                              }}
                              control={control}
                              name={`parts.${i}.quantity`}
                              render={({ field, fieldState }) => (
                                <div>
                                  {/* {JSON.stringify(fieldState)} */}

                                  <TextField
                                    {...field}
                                    error={!!fieldState.error}
                                    helperText={fieldState.error?.message}
                                    required
                                    size="small"
                                    type="number"
                                  ></TextField>
                                </div>
                              )}
                            />
                          </TableCell>
                          <TableCell>
                            <Controller
                              control={control}
                              name={`parts.${i}.price`}
                              render={({ field, fieldState }) => (
                                <TextField
                                  {...field}
                                  error={!!fieldState.error}
                                  helperText={fieldState.error?.message}
                                  inputProps={{
                                    min: 1,
                                  }}
                                  size="small"
                                  type="number"
                                ></TextField>
                              )}
                            />
                          </TableCell>
                          <TableCell>
                            {parts.length && (
                              <TextField
                                value={parts[i].price * parts[i].quantity}
                                size="small"
                              ></TextField>
                            )}
                          </TableCell>
                          <TableCell>السليمانية</TableCell>
                        </TableRow>
                      );
                    })}
                    <TableRow>
                      <TableCell>خدمات إضافية</TableCell>
                      <TableCell colSpan={4}>
                        <Controller
                          control={control}
                          name="services"
                          render={({ field }) => (
                            <TextField
                              {...field}
                              type="number"
                              className="w-full"
                              size="small"
                            ></TextField>
                          )}
                        />
                      </TableCell>
                    </TableRow>
                    <TableRow>
                      <TableCell>
                        <div className="flex">
                          <h4 className="font-bold text-xl">
                            القيمة الإجمالية
                          </h4>
                        </div>
                      </TableCell>

                      <TableCell colSpan={4}>
                        {parts.length && (
                          <Controller
                            name="coast"
                            control={control}
                            render={({ field }) => (
                              <TextField
                                {...field}
                                type="number"
                                size="small"
                                className="w-full"
                              ></TextField>
                            )}
                          />
                        )}
                      </TableCell>
                    </TableRow>
                  </TableBody>
                </Table>
              )}
              <Controller
                control={control}
                name="notes"
                render={({ field }) => (
                  <TextField
                    label="ملاحظات"
                    className="col-span-12"
                    multiline
                    {...field}
                  ></TextField>
                )}
              />
            </div>
          </DialogContent>
          <DialogActions>
            <Button type="submit">حفظ</Button>
            <Button onClick={() => props.onClose(false)}>اغلاق</Button>
          </DialogActions>
        </form>
      </Dialog>
    </div>
  );
};
