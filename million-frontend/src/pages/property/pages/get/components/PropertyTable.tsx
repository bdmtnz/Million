import { Column } from "primereact/column"
import { DataTable } from "primereact/datatable"
import type { PropertyFiltered } from "../../../Property.Model"
import { Utils } from "../../../../../shared/Utils"
import { Button } from "primereact/button"
import { useNavigate } from "react-router"

type PropertyTableProps = {
    properties: PropertyFiltered[]
}
const PropertyTable = ({ properties } : PropertyTableProps) => {    
    const navigate = useNavigate()

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

    return (        
        <DataTable value={properties} className="rounded-xl">
            <Column header="Image" body={imageBodyTemplate} className="w-24"></Column>
            <Column field="name" header="Name"></Column>
            <Column field="address" header="Address"></Column>
            <Column field="price" header="Price" body={priceBodyTemplate} className="w-30"></Column>
            <Column field="year" header="Year" className="w-24"></Column>
            <Column field="createdOnUtc" header="Created On" body={createBodyTemplate} className="w-36"></Column>
            <Column header="Actions" body={actionsBodyTemplate} className="w-24"></Column>
        </DataTable>
    )
}

export default PropertyTable