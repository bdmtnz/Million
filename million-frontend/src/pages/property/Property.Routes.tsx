import GetProperty from "./pages/GetProperty";
import DetailProperty from "./pages/DetailProperty";

export const appPropertyChildren = [
    { index: true, Component: GetProperty },
    { path: ":propertyId", Component: DetailProperty }
]