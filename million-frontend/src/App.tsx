// import reactLogo from './assets/react.svg'
// import viteLogo from '/vite.svg'
import { Outlet } from 'react-router'
import './App.css'
import AppToolbar from './shared/components/AppToolbar'

function App() {

  return (
    <>
      <AppToolbar/>
      <Outlet />
    </>
  )
}

export default App
