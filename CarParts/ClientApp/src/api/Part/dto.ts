export interface GetPartsDTO {
    id: number;
    name: string;
    description: string;
    categoryId: number;
}

export class AddPartDTO {
    name = '';
    description = '';
    categoryId = ''
}

