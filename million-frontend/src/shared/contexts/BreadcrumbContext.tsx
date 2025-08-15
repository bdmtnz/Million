import { createContext, useContext, useState } from 'react';
import type { BreadcrumbState } from '../models/Components';
import type { MenuItem } from 'primereact/menuitem';

const initial: BreadcrumbState = {
    items: [],
    set: () => {}
}

const BreadcrumbContext = createContext(initial);

export const BreadcrumbProvider = ({ children }: any) => {
    const [items, setItems] = useState<MenuItem[]>([])

    const setBreadcrumb = (param: MenuItem[]) => setItems(param)

    const value = { 
        items,
        set: setBreadcrumb,
    } as BreadcrumbState

    return (
        <BreadcrumbContext.Provider value={value}>
            {children}
        </BreadcrumbContext.Provider>
    )
}

export const useBreadcrumbs = () => useContext(BreadcrumbContext)