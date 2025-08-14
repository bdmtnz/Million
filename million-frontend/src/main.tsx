import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
// import 'primereact/resources/themes/bootstrap4-dark-blue/theme.css';
import { PrimeReactProvider } from 'primereact/api'
import { RouterProvider } from 'react-router'
import { appRouter } from './Routes.tsx'

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <PrimeReactProvider>
      <RouterProvider router={appRouter}/>
    </PrimeReactProvider>
  </StrictMode>,
)
