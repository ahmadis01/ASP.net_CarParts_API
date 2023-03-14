export class AddCarDTO {
    name = ''
    brandId = ''
    model?: string = ''
    image?: File | null = null
    imageUrl: string = ''
    carCategoryId?= ''

}

export class GetAllCar {
    id = '';
    name = '';
    model = '';
    image = '';
    brandId = '';
    carCategoryId? = '';
}

