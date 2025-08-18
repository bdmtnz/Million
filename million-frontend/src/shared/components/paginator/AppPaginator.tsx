import { Dropdown } from "primereact/dropdown";
import { Paginator, type PaginatorPageChangeEvent } from "primereact/paginator";

export type AppPaginatorProps = {
    first: number,
    total: number,
    pageSize: number,
    setFirst: (first: number) => void,
    setPageSize: (pageSize: number) => void,
    setPage: (page: number) => void
}
const AppPaginator = ({ first, total, pageSize, setFirst, setPageSize, setPage } : AppPaginatorProps) => {

    const onPageChange = (event: PaginatorPageChangeEvent) => {
        setFirst(event.first);
        setPageSize(event.rows);
        setPage(event.page + 1)
    };

    
    const dropdownOptions = [
        { label: 3, value: 3 },
        { label: 5, value: 5 },
        { label: 10, value: 10 }
    ];

    const template = {
        layout: 'RowsPerPageDropdown PrevPageLink PageLinks NextPageLink CurrentPageReport',
        RowsPerPageDropdown: (options:any) => {
            return (
                <div className="align-items-center hidden sm:flex">
                    <Dropdown value={options.value} options={dropdownOptions} onChange={options.onChange} className="w-25"/>
                </div>
            )
        },
        CurrentPageReport: (options:any) => {
            return (
                <span className="w-25 text-sm text-white text-center">
                    {options.first} - {options.last} de {options.totalRecords}
                </span>
            )
        }
    };

    return (
        <div className="card">
            <Paginator template={template} first={first} rows={pageSize} totalRecords={total} rowsPerPageOptions={[5, 10, 15]} pageLinkSize={3} onPageChange={onPageChange} />
        </div>
    );
}

export default AppPaginator;