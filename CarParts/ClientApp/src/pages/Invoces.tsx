import { CustomerApi } from "@/api/Customer";
import { CustomerItem } from "@/api/Customer/GetAll";
import { AddInvoiceDto } from "@/api/Invoice/AddInvoiceDto";
import CreateInvoice from "@/components/invoice/CreateInvoice";
import { Button } from "@mui/material";
import React, { useRef, useState } from "react";
import { useQuery } from "react-query";

function Invoces() {
  const [invoices, setInvoices] = useState<any[]>([]);
  const [customers, setCustomers] = useState<CustomerItem[]>([]);
  const [invoiceDialog, setInvoiceDialog] = useState(false);
  const customerQuery = useQuery({
    queryFn: CustomerApi.fetchCustomers,
    queryKey: "customer",
    onSuccess: (data) => {
      setCustomers(data);
    },
  });
  return (
    <div>
      <Button onClick={() => setInvoiceDialog(true)}>إنشاء فاتورة</Button>
      <CreateInvoice
        onSubmit={() => {}}
        parts={[]}
        customers={customers}
        onClose={(e) => setInvoiceDialog(e)}
        is={invoiceDialog}
      ></CreateInvoice>
    </div>
  );
}

export default Invoces;
