export interface GetPartsDTO {
    id: number;
    name: string;
    description: string;
    categoryId: number;
}

export class AddPartDTO {
    name = '';
    code = '';
    description = '';
    categoryId = '';
    brandId = '';
    storeId = '';
    quantity = 0;
    orginalPrice = 0;
    sellingPrice = 0;
    image: File | null = null
    carIds: string[] = []
}

