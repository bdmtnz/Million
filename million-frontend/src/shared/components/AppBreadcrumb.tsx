import { BreadCrumb } from "primereact/breadcrumb"
import { useBreadcrumbs } from "../contexts/BreadcrumbContext";
import { Link } from "react-router";


const AppBreadcrumb = () => {
    const {items} = useBreadcrumbs()

    const home = {
        template: () => <Link to="/"><i className="pi pi-home" /></Link>
    };
  
    return (
        <div className="card bg-[#374151] flex justify-center">
            <div className="w-full max-w-7xl">
                <BreadCrumb model={items} home={home} />
            </div>
        </div>
    )
}

export default AppBreadcrumb