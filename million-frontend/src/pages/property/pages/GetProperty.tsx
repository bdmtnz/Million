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

const GetProperty = () => {
    const [page, setPage] = useState(1);
    const [pageSize, setPageSize] = useState(5);
    const [total, setTotal] = useState(0);
    const [properties, setProperties] = useState<PropertyFiltered[]>([])

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

    const formatCurrency = (value) => {
        return value.toLocaleString('en-US', { style: 'currency', currency: 'USD' })
    };

    const formatDate = (value) => {
        return value.split('T')[0]
    };

    const imageBodyTemplate = (product) => (
        <div className="w-16 aspect-square">
            <img className="w-full h-full object-cover rounded-xl" src={product.image} alt='property-img' />
        </div>
    )

    const priceBodyTemplate = (product) => (
        <div>
            <p className="text-right">
                {formatCurrency(product.price)}
            </p>
        </div>
    )

    const createBodyTemplate = (product) => <p>{formatDate(product.createdOnUtc)}</p>

    const actionsBodyTemplate = (product) => {
        return (
            <Button icon="pi pi-eye" rounded outlined />
        )
    };

    const header = (
        <div className="flex flex-wrap align-items-center justify-content-between gap-2">
            <IconField iconPosition="left">
                <InputIcon className="pi pi-search" />
                <InputText placeholder="Search" />
            </IconField>
        </div>
    );

    const footer = () => <AppPaginator total={total} pageSize={pageSize} setPageSize={setPageSize} setPage={setPage} />

    return (
        <div className="card">
            <DataTable value={properties} header={header} footer={footer} tableStyle={{ minWidth: '40rem' }}>
                <Column header="Image" body={imageBodyTemplate} className="w-24"></Column>
                <Column field="name" header="Name"></Column>
                <Column field="address" header="Address"></Column>
                <Column field="price" header="Price" body={priceBodyTemplate} className="w-30"></Column>
                <Column field="year" header="Year" className="w-24"></Column>
                <Column field="createdOnUtc" header="Created At" body={createBodyTemplate} className="w-36"></Column>
                <Column header="Actions" body={actionsBodyTemplate} className="w-24"></Column>
            </DataTable>
        </div>
    );
}

export default GetProperty