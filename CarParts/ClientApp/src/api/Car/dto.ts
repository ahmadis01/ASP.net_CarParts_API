export class AddCarDTO {
    name = ''
    brandId = ''
    model?: string = ''
    image?: File | null = null
    carCategoryId?= ''
}

export interface GetAllCar {
    id: number;
    name: string;
    model: number;
    image: string;
    brandId: number;
    carCategoryId?: any;
}