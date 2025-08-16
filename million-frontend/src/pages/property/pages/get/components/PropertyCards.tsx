import type { PropertyFiltered } from "../../../Property.Model"

type PropertyCardsProps = {
    properties: PropertyFiltered[]
}
const PropertyCards = ({ properties }: PropertyCardsProps) => {
    return (
        <>
            <div>
                <div className="flex gap-4 bg-[#374151] rounded-md shadow-xl">
                    <div className="relative w-full max-w-sm sm:max-w-md md:max-w-lg bg-white overflow-hidden rounded-l-md">
                        <img
                            className="w-full h-48 object-cover"
                            src="https://media.istockphoto.com/id/1255835530/photo/modern-custom-suburban-home-exterior.jpg?s=612x612&w=0&k=20&c=0Dqjm3NunXjZtWVpsUvNKg2A4rK2gMvJ-827nb4AMU4="
                            alt="House 4"
                        />
                        <div className="absolute inset-0 bg-black bg-opacity-30 p-6 flex flex-col justify-end text-white">
                            <h2 className="text-2xl sm:text-3xl font-bold mb-1">House 4</h2>
                            <p className="text-xl sm:text-2xl font-bold mb-2">$1,000,000</p>
                            <div className="text-sm font-medium">
                                <p>Address 4</p>
                                <p>2003</p>
                            </div>
                        </div>
                    </div>

                    <div className="w-100 max-w-sm sm:max-w-md md:max-w-lg p-6">
                        <div className="flex items-center space-x-4">
                            <img
                                className="w-16 h-16 rounded-full object-cover"
                                src="https://i.imgur.com/HYUPOUX.jpeg"
                                alt="Owner 4"
                            />
                            <div>
                                <h3 className="text-2xl font-bold text-gray-900">Owner 4</h3>
                                <p className="text-gray-500">Owner Address 4</p>
                                <p className="text-gray-500">DOB: 01/01/180</p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </>
    )
}

export default PropertyCards