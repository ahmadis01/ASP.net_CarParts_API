import AddInvoice from '@/components/invoice/AddInvoice'
import { Button } from '@mui/material';
import React, { useRef, useState } from 'react'

function Invoces() {
    const [invoiceDialog, setInvoiceDialog] = useState(false);
    const dialogRef = useRef(null)
    return (
        <div>
            <Button onClick={() => setInvoiceDialog(true)}>إنشاء فاتورة</Button>
            <AddInvoice ref={dialogRef} onClose={(e) => setInvoiceDialog(e)} is={invoiceDialog}></AddInvoice>
        </div>
    )
}

export default Invoces