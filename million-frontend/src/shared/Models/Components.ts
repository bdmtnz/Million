import type { MenuItem } from "primereact/menuitem"

export type BreadcrumbState = {
    items: MenuItem[],
    set: (items:MenuItem[]) => void
}