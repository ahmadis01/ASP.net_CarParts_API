export class AddCarPartDto {
    partId = '';
    carId = [];
    brandId = '';
    storeId = '';
    quantity = 1;
    orginalPrice = 0;
    sellingPrice = 0;
    image: File | null = null;
}