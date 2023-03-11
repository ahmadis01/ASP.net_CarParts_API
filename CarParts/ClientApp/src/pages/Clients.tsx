import React from 'react'
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import Table from '@mui/material/Table';
import TableCell from '@mui/material/TableCell';
import TableRow from '@mui/material/TableRow';
import TableBody from '@mui/material/TableBody';
import { IconButton } from '@mui/material';
import { MoreVert } from '@mui/icons-material';

function Clients() {
    return (
        <TableContainer >
            <Table sx={{ minWidth: 650 }} aria-label="simple table">
                <TableHead>
                    <TableRow>
                        <TableCell>رقم الزبون</TableCell>
                        <TableCell align="right">اسم الزبون</TableCell>
                        <TableCell align="right">رقم الهاتف</TableCell>

                    </TableRow>
                </TableHead>
                {/* <TableBody>
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
                </TableBody> */}
            </Table>
        </TableContainer>
    )
}

export default Clients