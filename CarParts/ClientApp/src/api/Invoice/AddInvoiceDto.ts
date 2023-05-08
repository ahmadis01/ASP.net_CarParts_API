class AddInvoiceDto {
  date: string = "";
  coast: number = 0;
  clientId: string = '';
  isImport: boolean = false;
  notes: string = "";
  services: number = 0;
  parts: Part[] = [];
}

interface Part {
  partId: number;
  storeId: number;
  quantity: number;
  price: number;
}

export { AddInvoiceDto };
