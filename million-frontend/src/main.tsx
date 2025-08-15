import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import { PrimeReactProvider } from 'primereact/api'
import { RouterProvider } from 'react-router'
import { appRouter } from './Routes.tsx'
import { BreadcrumbProvider } from './shared/contexts/BreadcrumbContext.tsx'

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <PrimeReactProvider>
      <BreadcrumbProvider>
        <RouterProvider router={appRouter}/>
      </BreadcrumbProvider>
    </PrimeReactProvider>
  </StrictMode>,
)
