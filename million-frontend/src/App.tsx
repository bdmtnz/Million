// import reactLogo from './assets/react.svg'
// import viteLogo from '/vite.svg'
import { Link, Outlet } from 'react-router'
import './App.css'
import AppToolbar from './shared/components/AppToolbar'
import { BreadCrumb } from 'primereact/breadcrumb'
import { useBreadcrumbs } from './shared/contexts/BreadcrumbContext'

function App() {
  const {items} = useBreadcrumbs()

  const home = {
    template: () => <Link to="/"><i className="pi pi-home" /></Link>
  };
  
  return (
    <>
      <AppToolbar/>
      <BreadCrumb model={items} home={home} />
      <div className='py-5 sm:py-10'>        
        <Outlet />
      </div>
    </>
  )
}

export default App
