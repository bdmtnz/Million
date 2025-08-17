import { useNavigate } from "react-router"
import { Utils } from "../../../../../shared/Utils"
import type { PropertyFiltered } from "../../../Property.Model"

type PropertyCardsProps = {
    properties: PropertyFiltered[]
}
const PropertyCards = ({ properties }: PropertyCardsProps) => {
    const navigate = useNavigate()
    
    return (
        <div className="flex flex-col gap-2">
            {
                properties.map(p => (
                    <div
                        onClick={() => {
                            navigate(`/${p.id}`)
                        }}>
                        <div className="flex bg-[#374151] rounded-md shadow-2xl">
                            <div className="relative w-full overflow-hidden rounded-md sm:rounded-r-none border-5 border-[#374151]">
                                <img
                                    className="w-full h-50 object-cover"
                                    src={p.image}
                                    alt="house-img"
                                />
                                <div className="absolute inset-y-0 left-0 w-1/2 bg-gradient-to-r from-[#121212] to-transparent"></div>
                                <div className="absolute inset-0 bg-opacity-30 p-6 flex items-end justify-between text-white">
                                    <div>
                                        <h2 className="text-2xl sm:text-3xl font-bold mb-1">{p.name}</h2>
                                        <p className="text-xl sm:text-2xl font-bold mb-2">{Utils.formatCurrency(p.price)}</p>
                                        <div className="text-sm font-medium">
                                            <p className="text-gray-300">{p.address}</p>
                                            <p className="text-gray-300">{p.year}</p>
                                        </div>
                                    </div>
                                    <img
                                        className="w-16 h-16 rounded-full object-cover sm:hidden border-3 border-white shadow-2xl"
                                        src={p.owner.photo}
                                        alt="Owner 4"
                                    />
                                </div>
                            </div>

                            <div className="w-50 p-6 flex-col items-center justify-center hidden sm:flex">
                                <div className="flex flex-col items-center gap-2">
                                    <img
                                        className="w-16 h-16 rounded-full object-cover"
                                        src={p.owner.photo}
                                        alt="Owner 4"
                                    />
                                    <div>
                                        <h3 className="text-xl font-bold text-center">{p.owner.name}</h3>
                                        <p className="text-gray-300 text-center text-sm">{p.owner.address}</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                ))
            }
        </div>
    )
}

export default PropertyCards