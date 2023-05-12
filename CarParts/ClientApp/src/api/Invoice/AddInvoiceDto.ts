class AddInvoiceDto {
  date: string|Date = "";
  coast: number = 0;
  clientId: string = '';
  isImport: boolean = false;
  received = true;
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
