import { GetAllCar } from '@/api/Car/dto'
import React, { useMemo, useRef, useState } from 'react'
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import CardMedia from '@mui/material/CardMedia';
import Typography from '@mui/material/Typography';
import { Button, CardActionArea, CardActions } from '@mui/material';
import { SERVER_URL } from '@/../app.config';
import { useSelector } from 'react-redux';
import { RootState } from '@/store';
import { BrandItem } from '@/api/Brand/dto';

type propsType = {
    carsList: GetAllCar[],
    onDetails: (carItem: GetAllCar) => void
}
export default function CarsList(props: propsType) {
    const brands = useSelector<RootState, BrandItem[]>(state => state.brand.brands)
    const brandInfo = (id: number) => brands.find(b => b.id == id)
    const getFileUrl = (url: string) => `${SERVER_URL}/${url}`
    return (
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 2xl:grid-cols-4 gap-4">
            {
                props.carsList.map(car =>
                    <Card key={car.id} sx={{ borderRadius: '28px', padding: '6px' }} >
                        {car.image && <CardMedia
                            sx={{ height: '240px', borderRadius: '22px' }}
                            component="img"
                            image={getFileUrl(car.image)}
                            alt="green iguana"
                        />}

                        <CardContent className=''>
                            <div className="flex justify-between items-center">

                                {brands.length && <img src={`/brands/${brandInfo(car.brandId)?.name}.png`} className='h-14' alt="" />}
                                <Typography className='text-gray-700' fontWeight={600} gutterBottom variant="h5" margin={0} component="div">
                                    {brandInfo(car.brandId)?.name} {car.name}
                                </Typography>
                            </div>
                            <Typography className='text-gray-500' fontSize={14} align='right'>{car.model}</Typography>

                        </CardContent>

                        <CardActions>
                            <Button variant='contained' fullWidth sx={{ borderRadius: '24px' }} onClick={() => props.onDetails(car)}>التفاصيل</Button>
                        </CardActions>
                    </Card>
                )

            }

        </div>

    );


}
