import { Outlet } from "react-router"


const AppProperty = () => {
    return (
        <div className="flex flex-col gap-8">
            <div className="page-header">
                <h1 className="text-2xl font-bold">
                    Properties
                </h1>
                <h3>Here you can management the properties</h3>
            </div>
            <Outlet/>
        </div>
    )
}

export default AppProperty