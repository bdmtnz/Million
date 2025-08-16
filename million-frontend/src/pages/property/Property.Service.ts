import axios from "axios";
import type { Property, PropertyFiltered } from "./Property.Model";
import type { Pagination } from "../../shared/models/Responses";

const baseURL = "https://localhost:7257";

export const PropertyService = {
    get: (keyword: string | null = null, limit: number = 5, offset: number = 1, from: number | null = null, to: number | null = null) => {
        return axios.get<Pagination<PropertyFiltered>>(`${baseURL}/api/Property?keyword=${keyword??''}&limit=${limit??''}&offset=${offset??''}&from=${from??''}&to=${to??''}`)
    },
    getById: (id: string) => axios.get<Property>(`${baseURL}/api/Property/${id}`)
}