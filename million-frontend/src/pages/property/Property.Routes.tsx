import GetProperty from "./pages/get/GetProperty";
import DetailProperty from "./pages/DetailProperty";
import { PropertyService } from "./Property.Service";

export const appPropertyChildren = [
    { index: true, Component: GetProperty },
    { 
        path: ":propertyId", 
        loader: async ({ params }: any) => {
            const response = await PropertyService.getById(params.propertyId)
            return { ...response.data }
        },
        Component: DetailProperty,
    }
]