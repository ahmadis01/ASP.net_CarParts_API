import { axiosIns } from "@/libs/axios";
import axios from "axios";
import { AddInvoiceDto } from "./AddInvoiceDto";

enum InvoiceEndPoints {
  Base = "/Invoice",
}

export class InvoiceApi {
  static async GetAll() {
    return await axiosIns.get<any[]>(InvoiceEndPoints.Base);
  }

  static async CreateInvoice(payload: AddInvoiceDto) {
    const {data} = await axiosIns.post<AddInvoiceDto, any>(
      InvoiceEndPoints.Base,
      payload
    );
    return data
  }
}
