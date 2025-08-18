// import reactLogo from './assets/react.svg'
// import viteLogo from '/vite.svg'
import { Outlet } from 'react-router'
import './App.css'
import AppToolbar from './shared/components/toolbar/AppToolbar'
import AppBreadcrumb from './shared/components/breadcrumb/AppBreadcrumb'

function App() {
  return (
    <>
      <AppToolbar/>
      <AppBreadcrumb/>
      <div className='py-5 sm:py-10'>        
        <Outlet />
      </div>
    </>
  )
}

export default App
