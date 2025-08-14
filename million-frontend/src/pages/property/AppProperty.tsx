import { Outlet } from "react-router"


const AppProperty = () => {
    return (
        <>
            <h1 className="text-3xl font-bold underline">
                Hello Property Page!
            </h1>
            <Outlet/>
        </>
    )
}

export default AppProperty