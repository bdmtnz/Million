import { Outlet } from "react-router"


const AppProperty = () => {
    return (
        <>
            <h1 className="text-2xl font-bold">
                Properties
            </h1>
            <h3>Here you can management the properties</h3>
            <Outlet/>
        </>
    )
}

export default AppProperty