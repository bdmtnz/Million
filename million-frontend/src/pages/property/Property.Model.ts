type PropertyOwner = {
    name: string,
    address: string,
    photo: string,
    bornOnUtc: Date
}

export type PropertyFiltered = {
    id: string,
    name: string,
    address: string,
    price: number,
    code: string,
    year: number,
    image: string,
    owner: PropertyOwner,
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
    owner: PropertyOwner,
    trace: {
        name: string,
        value: number,
        saledOnUtc: Date
    },
    createdOnUtc: Date,
}
