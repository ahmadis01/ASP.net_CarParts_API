import { Button, Dialog, DialogActions, DialogTitle, Modal } from '@mui/material'
import React from 'react'


interface Props {
    is: boolean,
    onClose: (is: boolean) => void
}



export default React.forwardRef((props: Props) => {
    return (
        <div>
            <Dialog open={props.is} >
                <DialogTitle>
                    إنشاء فاتورة
                </DialogTitle>

                <div>
                    مرحبااا
                </div>
                <DialogActions>
                    <Button>حفظ</Button>
                    <Button onClick={() => props.onClose(false)}>اغلاق</Button>
                </DialogActions>
            </Dialog>

        </div>
    )
})
