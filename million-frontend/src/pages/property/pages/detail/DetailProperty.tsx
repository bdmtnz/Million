import { Link, useLoaderData } from "react-router";
import { Utils } from "../../../../shared/Utils";
import { useEffect } from "react";
import { useBreadcrumbs } from "../../../../shared/contexts/BreadcrumbContext";

const DetailProperty = () => {
    const {set} = useBreadcrumbs()
    const data = useLoaderData() 

    useEffect(() => {
        set([
            {
                label: 'Properties',
                template: () => <Link to="/" className="text-primary font-semibold">Properties</Link>
            },
            {
                label: 'Detail'
            }
        ])
    }, [])
    
    return (
        <>
            <div className="relative w-full h-70">
                <img className="absolute inset-0 w-full h-full object-cover opacity-50 sm:opacity-25" src={data.image} alt="Fondo borroso"/>
                <div className="relative top-0 left-0 w-full h-full backdrop-blur-xs flex items-center justify-center">
                    <div className="relative w-70 aspect-square">
                        <img className="w-full h-full object-cover" src={data.image} alt='property-img' />
                    </div>
                </div>
                <div className="relative w-full h-10 left-0 -top-10 flex justify-center">
                    <div className="w-full max-w-7xl px-10 sm:px-20">
                        <h1 className="text-2xl font-bold">{data.name}</h1>
                    </div>
                </div>
            </div>
            <div className="w-full flex justify-center">
                <div className="px-10 pb-4 sm:px-20 w-full max-w-7xl">
                    <div className="flex flex-col gap-10">
                        <div>
                            <h2 className="text-xl font-semibold border-b-2 border-gray-200 pb-2 mb-4">Property details</h2>
                            <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                                <div>
                                    <p className="text-gray-300">Address:</p>
                                    <p className="font-medium text-gray-50">{data.address}</p>
                                </div>
                                <div>
                                    <p className="text-gray-300">Builded On:</p>
                                    <p className="font-medium text-gray-50">{data.year}</p>
                                </div>
                                <div className="col-span-1 md:col-span-2">
                                    <p className="text-gray-300">Price:</p>
                                    <p className="text-3xl font-bold text-emerald-600">{Utils.formatCurrency(data.price)}</p>
                                </div>
                            </div>
                            <div className="mt-6 text-xs text-gray-400">
                                <p className="flex gap-4">
                                    <span className="w-10">ID:</span>
                                    <span className="line-clamp-1 text-ellipsis">{data.id}</span>
                                </p>
                                <p className="flex gap-4">
                                    <span className="w-10">Code:</span>
                                    <span className="line-clamp-1 text-ellipsis">{data.code}</span>
                                </p>
                            </div>
                        </div>
                        <div>
                            <h2 className="text-xl font-semibold border-b-2 border-gray-200 pb-2 mb-4">Owner details</h2>
                            <div className="flex gap-8">
                                <div className="flex items-center space-x-4">
                                    <img className="w-16 h-16 rounded-full object-cover border-2 border-gray-300" src={data.owner.photo} alt="Owner 4"/>
                                </div>
                                <div>
                                    <p className="text-lg font-medium text-gray-50">{data.owner.name}</p>
                                    <p className="text-sm text-gray-300">{data.owner.address}</p>
                                    <p className="text-sm text-gray-300">Born On: {Utils.formatDate(data.owner.bornOnUtc)}</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </>
    )
}

export default DetailProperty