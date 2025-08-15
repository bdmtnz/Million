import { Paginator, type PaginatorPageChangeEvent } from "primereact/paginator";
import { useState } from "react";


export type AppPaginatorProps = {
    total: number,
    pageSize: number,
    setPageSize: (pageSize: number) => void,
    setPage: (page: number) => void
}
const AppPaginator = ({ total, pageSize, setPageSize, setPage } : AppPaginatorProps) => {
    const [first, setFirst] = useState(1);

    const onPageChange = (event: PaginatorPageChangeEvent) => {
        setFirst(event.first);
        setPageSize(event.rows);
        setPage(event.page + 1)
    };

    return (
        <div className="card">
            <Paginator first={first} rows={pageSize} totalRecords={total} rowsPerPageOptions={[5, 10, 15]} pageLinkSize={3} onPageChange={onPageChange} />
        </div>
    );
}

export default AppPaginator;