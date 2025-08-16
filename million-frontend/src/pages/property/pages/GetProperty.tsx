import { Column } from "primereact/column";
import { DataTable } from "primereact/datatable";
import { useEffect, useState, type ChangeEvent } from "react";
import { PropertyService } from "../Property.Service";
import type { PropertyFiltered } from "../Property.Model";
import { IconField } from "primereact/iconfield";
import { InputIcon } from "primereact/inputicon";
import { InputText } from "primereact/inputtext";
import { Button } from "primereact/button";
import AppPaginator from "../../../shared/components/AppPaginator";
import { useNavigate } from "react-router";
import { Utils } from "../../../shared/Utils";
import { useBreadcrumbs } from "../../../shared/contexts/BreadcrumbContext";
import { InputNumber, type InputNumberValueChangeEvent } from "primereact/inputnumber";

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
    
    const navigate = useNavigate()

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

    const imageBodyTemplate = (property: PropertyFiltered) => (
        <div className="w-16 aspect-square">
            <img className="w-full h-full object-cover rounded-xl" src={property.image} alt='property-img' />
        </div>
    )

    const priceBodyTemplate = (property: PropertyFiltered) => (
        <div>
            <p className="text-right">
                {Utils.formatCurrency(property.price)}
            </p>
        </div>
    )

    const createBodyTemplate = (property: PropertyFiltered) => <p>{Utils.formatDate(property.createdOnUtc)}</p>

    const actionsBodyTemplate = (property: PropertyFiltered) => {
        return (
            <Button icon="pi pi-eye" rounded outlined onClick={() => {
                navigate(`/${property.id}`)
            }}/>
        )
    }

    const handleInputChange = (e: ChangeEvent<HTMLInputElement>) => {
        setFilter({...filter, keyword: e.target.value})
    }

    return (
        <div className="w-full flex flex-col items-center">
            <div className="card px-10 sm:px-20 flex flex-col gap-4 w-full max-w-7xl">
                <div className="px-4">
                    <div className="flex gap-2">
                        <div>
                            <span className="text-xs">Keyword</span>
                            <fieldset className="bg-gray-700 rounded-md p-1 flex gap-1">
                                <IconField iconPosition="left">
                                    <InputIcon className="pi pi-search" />
                                    <InputText
                                        key="id-32131231313SW"
                                        placeholder="Search"
                                        type="text"
                                        value={filter.keyword}
                                        onChange={handleInputChange}
                                    />
                                </IconField>
                            </fieldset>
                        </div>
                        <div>
                            <span className="text-xs">Price</span>
                            <fieldset className="bg-gray-700 rounded-md p-1 flex gap-1">
                                <InputNumber 
                                    value={filter.min}
                                    onValueChange={(e: InputNumberValueChangeEvent) => setFilter({ ...filter, min: e.value ?? null })} 
                                    mode="currency" 
                                    currency="USD" 
                                    placeholder="Min"
                                    size={6}
                                />
                                <InputNumber 
                                    value={filter.max} 
                                    onValueChange={(e: InputNumberValueChangeEvent) => setFilter({ ...filter, max: e.value ?? null })} 
                                    mode="currency" 
                                    currency="USD" 
                                    placeholder="Max"
                                    size={6}
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
                <DataTable value={properties}>
                    <Column header="Image" body={imageBodyTemplate} className="w-24"></Column>
                    <Column field="name" header="Name"></Column>
                    <Column field="address" header="Address"></Column>
                    <Column field="price" header="Price" body={priceBodyTemplate} className="w-30"></Column>
                    <Column field="year" header="Year" className="w-24"></Column>
                    <Column field="createdOnUtc" header="Created On" body={createBodyTemplate} className="w-36"></Column>
                    <Column header="Actions" body={actionsBodyTemplate} className="w-24"></Column>
                </DataTable>           
                <AppPaginator total={total} pageSize={pageSize} setPageSize={setPageSize} setPage={setPage} />
            </div>
        </div>
    );
}

export default GetProperty