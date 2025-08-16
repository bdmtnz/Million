import { Dropdown } from "primereact/dropdown";
import { Paginator, type PaginatorPageChangeEvent } from "primereact/paginator";
import { useState } from "react";


export type AppPaginatorProps = {
    total: number,
    pageSize: number,
    setPageSize: (pageSize: number) => void,
    setPage: (page: number) => void
}
const AppPaginator = ({ total, pageSize, setPageSize, setPage } : AppPaginatorProps) => {
    const [first, setFirst] = useState(0);

    const onPageChange = (event: PaginatorPageChangeEvent) => {
        setFirst(event.first);
        setPageSize(event.rows);
        setPage(event.page + 1)
    };

    
    const dropdownOptions = [
        { label: 5, value: 5 },
        { label: 10, value: 10 },
        { label: 15, value: 15 }
    ];

    const template = {
        layout: 'RowsPerPageDropdown PrevPageLink PageLinks NextPageLink CurrentPageReport',
        RowsPerPageDropdown: (options:any) => {
            return (
                <div className="flex align-items-center">
                    <Dropdown value={options.value} options={dropdownOptions} onChange={options.onChange} className="w-25"/>
                </div>
            );
        },
        CurrentPageReport: (options:any) => {
            return (
                <span className="w-25 text-sm text-white">
                    {options.first} - {options.last} de {options.totalRecords}
                </span>
            );
        }
    };

    return (
        <div className="card">
            <Paginator template={template} first={first} rows={pageSize} totalRecords={total} rowsPerPageOptions={[5, 10, 15]} pageLinkSize={3} onPageChange={onPageChange} />
        </div>
    );
}

export default AppPaginator;