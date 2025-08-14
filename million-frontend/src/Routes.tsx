import { createBrowserRouter } from "react-router";
import App from "./App";
import AppProperty from "./pages/property/AppProperty";
import { appPropertyChildren } from "./pages/property/Property.Routes";

export const appRouter = createBrowserRouter([
  {
    path: "/",
    element: <App/>,
    children: [
        // {
        //     index: true,
        //     Component: AppProperty
        // },
        {
            path: '',
            Component: AppProperty,
            children: [...appPropertyChildren]
        }
    ]
  },
]);