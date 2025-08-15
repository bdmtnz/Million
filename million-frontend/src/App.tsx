// import reactLogo from './assets/react.svg'
// import viteLogo from '/vite.svg'
import { Outlet } from 'react-router'
import './App.css'
import AppToolbar from './shared/components/AppToolbar'

function App() {

  return (
    <>
      <AppToolbar/>
      <div className='px-10 py-5 sm:px-20 sm:py-10'>        
        <Outlet />
      </div>
    </>
  )
}

export default App
