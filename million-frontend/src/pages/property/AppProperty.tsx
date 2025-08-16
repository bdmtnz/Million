import { Outlet } from "react-router"


const AppProperty = () => {
    return (
        <div className="flex flex-col gap-8">
            <div className="w-full flex flex-col items-center">
                <div className="page-header px-10 sm:px-20 w-full max-w-7xl">
                    <h1 className="text-2xl font-bold">
                        Properties
                    </h1>
                    <h3>Here you can management the properties</h3>
                </div>
            </div>
            <Outlet/>
        </div>
    )
}

export default AppProperty