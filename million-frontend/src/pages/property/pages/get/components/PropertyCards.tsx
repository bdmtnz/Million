import { Utils } from "../../../../../shared/Utils"
import type { PropertyFiltered } from "../../../Property.Model"

type PropertyCardsProps = {
    properties: PropertyFiltered[]
}
const PropertyCards = ({ properties }: PropertyCardsProps) => {
    return (
        <div className="flex flex-col gap-2">
            {
                properties.map(p => (
                    <div>
                        <div className="flex bg-[#374151] rounded-md shadow-xl">
                            <div className="relative w-full bg-white overflow-hidden rounded-md sm:rounded-r-none">
                                <img
                                    className="w-full h-40 object-cover"
                                    src={p.image}
                                    alt="house-img"
                                />
                                <div className="absolute inset-0 bg-black bg-opacity-30 p-6 flex items-end justify-between text-white">
                                    <div>
                                        <h2 className="text-2xl sm:text-3xl font-bold mb-1">{p.name}</h2>
                                        <p className="text-xl sm:text-2xl font-bold mb-2">{Utils.formatCurrency(p.price)}</p>
                                        <div className="text-sm font-medium">
                                            <p className="text-gray-300">{p.address}</p>
                                            <p className="text-gray-300">{p.year}</p>
                                        </div>
                                    </div>
                                    <img
                                        className="w-16 h-16 rounded-full object-cover sm:hidden"
                                        src="https://i.imgur.com/HYUPOUX.jpeg"
                                        alt="Owner 4"
                                    />
                                </div>
                            </div>

                            <div className="w-50 p-6 flex-col items-center justify-center hidden sm:flex">
                                <div className="flex flex-col items-center gap-2">
                                    <img
                                        className="w-16 h-16 rounded-full object-cover"
                                        src="https://i.imgur.com/HYUPOUX.jpeg"
                                        alt="Owner 4"
                                    />
                                    <div>
                                        <h3 className="text-xl font-bold text-center">Owner 4</h3>
                                        <p className="text-gray-300 text-center text-sm">Owner Address 4</p>
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