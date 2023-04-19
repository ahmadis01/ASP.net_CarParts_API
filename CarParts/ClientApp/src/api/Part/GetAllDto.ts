export class GetAllPartsParams {
  CountryId: null | string = null;
  CarId: null | string = null;
  BrandId: null | string = null;
  StoreId: null | string = null;
  PageSize = 10;
  PageNumber = 1;
  OrderBy: null | string = null;
  OrderStatus: null | string = null;
  Search: null | string = null;
  Date: string | null = null;

}


export interface PartItem {
  id: number;
  code: string;
  name: string;
  description: string;
  cars: string[];
  storeParts: StorePart[];
  categoryId: number;
  brandId: number;
  orginalPrice: number;
  sellingPrice: number;
  image: string;
  createdAt: string;
}

export interface StorePart {
  storeId: number;
  quantity: number;
}



export interface GetAllParts {
  totalNumber: number;
  parts: PartItem[]
}