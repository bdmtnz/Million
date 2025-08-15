import { Column } from "primereact/column";
import { DataTable } from "primereact/datatable";
import { useEffect, useState } from "react";
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

const GetProperty = () => {    
    const {set} = useBreadcrumbs()
    const [page, setPage] = useState(1);
    const [pageSize, setPageSize] = useState(5);
    const [total, setTotal] = useState(0);
    const [properties, setProperties] = useState<PropertyFiltered[]>([])
    
    const navigate = useNavigate()

    useEffect(() => {
        set([
            {
                label: 'Properties'
            }
        ])     
    }, [])

    useEffect(() => {
        PropertyService.get(undefined, pageSize, page).then(response => {
            setProperties(response.data.page)
            setTotal(response.data.total)
        })
    }, [page])

    useEffect(() => {
        if (page !== 1) {
            setPage(1)
        }
        else {
            PropertyService.get(undefined, pageSize, page).then(response => {
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
    };

    const Header = () => (
        <div className="flex flex-wrap align-items-center justify-content-between gap-2">
            <IconField iconPosition="left">
                <InputIcon className="pi pi-search" />
                <InputText placeholder="Search" />
            </IconField>
        </div>
    );

    return (
        <div className="card px-10 sm:px-20 flex flex-col gap-4">
            <div className="px-4">
                <Header/>
            </div>
            <DataTable value={properties}>
                <Column header="Image" body={imageBodyTemplate} className="w-24"></Column>
                <Column field="name" header="Name"></Column>
                <Column field="address" header="Address"></Column>
                <Column field="price" header="Price" body={priceBodyTemplate} className="w-30"></Column>
                <Column field="year" header="Year" className="w-24"></Column>
                <Column field="createdOnUtc" header="Created At" body={createBodyTemplate} className="w-36"></Column>
                <Column header="Actions" body={actionsBodyTemplate} className="w-24"></Column>
            </DataTable>           
            <AppPaginator total={total} pageSize={pageSize} setPageSize={setPageSize} setPage={setPage} />
        </div>
    );
}

export default GetProperty