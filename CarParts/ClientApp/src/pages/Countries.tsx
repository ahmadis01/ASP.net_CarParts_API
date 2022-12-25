import React, { useEffect, useState } from 'react'
import { CountryController } from '~/api/Country'
import { axiosIns } from '../libs/axios'
import { useSelector, useDispatch } from 'react-redux'
import { fetchCountries } from '~/store/countries'
import { CountryItem } from '~/api/Country/dto'
import Box from '@mui/material/Box';
import Skeleton from '@mui/material/Skeleton';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import { IconButton } from '@mui/material';
import { MoreVert, MoreHoriz } from '@mui/icons-material'
import { AppDispatch, RootState } from '~/store'
function Loader() {
  return (
    <Box sx={{ width: '100%' }}>
      <Skeleton height={100} />
      <Skeleton height={75} animation="wave" />
      <Skeleton height={75} animation={false} />
      <Skeleton height={75} animation="wave" />
      <Skeleton height={75} animation={false} />
    </Box>
  );
}


function BasicTable(props: { data: CountryItem[] } = { data: [] }) {
  return (
    <TableContainer component={Paper}>
      <Table sx={{ minWidth: 650 }} aria-label="simple table">
        <TableHead>
          <TableRow>
            <TableCell>الدولة المصنعة</TableCell>
            <TableCell align="right">عدد السيارات</TableCell>
            <TableCell align="right">عدد القطع</TableCell>
            <TableCell align="right">عرض القطع</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {props.data.map((row) => (
            <TableRow
              key={row.name}
              sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
            >
              <TableCell component="th" scope="row">
                <div className="flex">

                  <img className='w-8' src={`/public/flags/${row.name}.svg`} alt="" />
                  <span className='mr-4'>

                    {row.name}
                  </span>
                </div>
              </TableCell>
              <TableCell align="right">
                {row.id}

              </TableCell>
              <TableCell align="right">{row.id}</TableCell>
              <TableCell align="right">

                <IconButton>
                  <MoreVert />
                </IconButton>

              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
}


function Countries() {
  const dispatch = useDispatch<AppDispatch>()


  const countries = useSelector<RootState>((state) => state.country.countries) as CountryItem[]
  const [isLoading, setLoading] = useState(false)




  useEffect(() => {
    // getCountries();
    dispatch(fetchCountries())
  }, [])
  return (
    <React.Suspense fallback={<Loader />}>

      <div>
        {
          isLoading &&
          <Loader />

        }

        {

          !isLoading &&



          < BasicTable data={countries} />

        }
      </div>
    </React.Suspense>

  )
}

export default Countries