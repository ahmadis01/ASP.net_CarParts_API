import React, { useEffect, useRef, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { AppDispatch, RootState } from "../store";

import "react-toastify/dist/ReactToastify.css";
import AddCar from "@/components/cars/AddCar";
import CarsList from "@/components/cars/CarsList";
import { GetAllCar } from "@/api/Car/dto";
import {
  Autocomplete,
  Card,
  FormControl,
  InputLabel,
  MenuItem,
  Select,
  TextField,
  Typography,
} from "@mui/material";
import { CountryItem } from "@/api/Country/dto";
import { CarApi } from "@/api/Car";
import { useQuery } from "react-query";
import { CarActions } from "@/store/cars";
export default function Cars() {
  const countries = useSelector<RootState, CountryItem[]>(
    (state) => state.country.countries
  );
  const brands = useSelector<RootState, CountryItem[]>(
    (state) => state.brand.brands
  );
  const [modifyItem, setModifyItem] = useState<GetAllCar | null>(null);

  useQuery("car", CarApi.fetchCars, {
    onSuccess: (data) => {
      dispatch(CarActions.setCarsList(data));
    },
  });

  const dispatch = useDispatch<AppDispatch>();

  const cars = useSelector<RootState, GetAllCar[]>((state) => state.car.cars);
  return (
    <div>
      <Card  className="p-4" elevation={0}>
        <div className="flex items-center gap-5 flex-wrap">
          <div className="basis-full">
            <Typography fontSize={24} fontWeight={'bold'} >
              السيارات
            </Typography>
          </div>
          <TextField className="flex-grow lg:grow-0 basis-full lg:basis-[300px]" size="small" label="ابحث عن سيارة معينة" />

          <FormControl  size="small" className="flex-grow lg:grow-0 basis-full lg:basis-[300px]">
            <InputLabel id="brand-id-label">الدولة</InputLabel>
            <Select value={''} labelId="brand-id-label" label="الدولة">
              {countries.map((c) => (
                <MenuItem key={c.id} value={c.id}>
                  {c.name}
                </MenuItem>
              ))}
            </Select>
          </FormControl>

          <FormControl size="small" className="flex-grow lg:grow-0 basis-full lg:basis-[300px]">
            <InputLabel id="brand-id-label">الشركة المصنعة</InputLabel>
            <Select value={''} labelId="brand-id-label" label="الشركة المصنعة">
              {brands.map((b) => (
                <MenuItem key={b.id} value={b.id}>
                  {b.name}
                </MenuItem>
              ))}
            </Select>
          </FormControl>

 

          <AddCar
            carModifyDto={modifyItem}
            onCloseDialog={() => setModifyItem(null)}
          ></AddCar>
        </div>
      </Card>

      <div className="mt-4">
        <CarsList
          onDetails={(car) => {
            setModifyItem(car);
          }}
          carsList={cars}
        ></CarsList>
      </div>
    </div>
  );
}
