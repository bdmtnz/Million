export type PropertyFiltered = {
    id: string,
    name: string,
    address: string,
    price: number,
    code: string,
    year: number,
    createdOnUtc: Date
}

export type Property = {
    id: string,
    name: string,
    address: string,
    price: number,
    code: string,
    year: number,
    image: string,
    owner: {
        name: string,
        address: string,
        photo: string,
        bornOnUtc: Date
    },
    trace: {
        name: string,
        value: number,
        saledOnUtc: Date
    },
    createdOnUtc: Date,
}
