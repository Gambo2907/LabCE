import React from 'react'
import { Navigate, Outlet } from 'react-router-dom'
import storage from '../Storage/storage'

//Para poner privada las rutas
export const ProtectedRoutes = ({ children}) => {
    //const authUser = storage.get('user-info');
    if(!localStorage.getItem('user-info')){
        return <Navigate to='/loginadministrador' />
    }
    return <Outlet />
  
}

export default ProtectedRoutes