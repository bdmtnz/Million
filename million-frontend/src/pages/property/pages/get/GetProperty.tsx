import { useEffect, useState, type ChangeEvent } from "react"
import { PropertyService } from "../../Property.Service"
import type { PropertyFiltered } from "../../Property.Model"
import { InputText } from "primereact/inputtext"
import { Button } from "primereact/button"
import AppPaginator from "../../../../shared/components/AppPaginator"
import { useBreadcrumbs } from "../../../../shared/contexts/BreadcrumbContext"
import { InputNumber, type InputNumberValueChangeEvent } from "primereact/inputnumber"
import PropertyTable from "./components/PropertyTable"
import PropertyCards from "./components/PropertyCards"

type Filter = {
    keyword: string| null,
    min: number | null,
    max: number | null
}

const filterInitial: Filter = {
    keyword: '',
    min: null,
    max: null
}

const GetProperty = () => {    
    const {set} = useBreadcrumbs()
    const [page, setPage] = useState(1);
    const [pageSize, setPageSize] = useState(5);
    const [total, setTotal] = useState(0);
    const [properties, setProperties] = useState<PropertyFiltered[]>([])
    const [filter, setFilter] = useState<Filter>(filterInitial)

    useEffect(() => {
        set([
            {
                label: 'Properties'
            }
        ])     
    }, [])

    useEffect(() => {
        PropertyService.get(filter.keyword, pageSize, page, filter.min, filter.max).then(response => {
            setProperties(response.data.page)
            setTotal(response.data.total)
        })
    }, [page])

    useEffect(() => {
        if (page !== 1) {
            setPage(1)
        }
        else {
            PropertyService.get(filter.keyword, pageSize, page, filter.min, filter.max).then(response => {
                setProperties(response.data.page)
                setTotal(response.data.total)
            })
        }
    }, [pageSize])

    const handleInputChange = (e: ChangeEvent<HTMLInputElement>) => {
        setFilter({...filter, keyword: e.target.value})
    }

    return (
        <div className="w-full flex flex-col items-center">
            <div className="card px-10 sm:px-20 flex flex-col gap-4 w-full max-w-7xl">
                <div>
                    <div className="grid gap-2 grid-cols-1 sm:grid-cols-2 xl:grid-cols-3">
                        <div className="w-full">
                            <span className="text-xs">Keyword</span>
                            <fieldset className="bg-gray-700 rounded-md p-1">                                
                                <InputText
                                    className="w-full"
                                    placeholder="Search"
                                    type="text"
                                    value={filter.keyword}
                                    onChange={handleInputChange}
                                />
                            </fieldset>
                        </div>
                        <div className="w-full flex justify-between gap-2">
                            <div className="w-full">
                                <span className="text-xs">Price</span>
                                <fieldset className="bg-gray-700 rounded-md p-1 gap-1 grid grid-cols-2">
                                    <InputNumber 
                                        value={filter.min}
                                        onValueChange={(e: InputNumberValueChangeEvent) => setFilter({ ...filter, min: e.value ?? null })} 
                                        mode="currency" 
                                        currency="USD"
                                        placeholder="Min"
                                        size={1}
                                    />
                                    <InputNumber 
                                        value={filter.max} 
                                        onValueChange={(e: InputNumberValueChangeEvent) => setFilter({ ...filter, max: e.value ?? null })} 
                                        mode="currency" 
                                        currency="USD" 
                                        placeholder="Max"
                                        size={1}
                                    />
                                </fieldset>
                            </div>
                            <div className="flex items-end">
                                <fieldset className="bg-gray-700 rounded-md p-1 flex gap-1 h-13.5">
                                    <Button icon="pi pi-search" severity="success" type="button" onClick={() => {
                                        PropertyService.get(filter.keyword, pageSize, page, filter.min, filter.max).then(response => {
                                            setProperties(response.data.page)
                                            setTotal(response.data.total)
                                        })
                                    }}/>
                                </fieldset>
                            </div>
                        </div>
                    </div>
                </div>
                <div className="hidden lg:inline">
                    <PropertyTable properties={properties} />
                </div>
                <div className="inline lg:hidden">
                    <PropertyCards properties={properties} />
                </div>
                <AppPaginator total={total} pageSize={pageSize} setPageSize={setPageSize} setPage={setPage} />
            </div>
        </div>
    );
}

export default GetProperty