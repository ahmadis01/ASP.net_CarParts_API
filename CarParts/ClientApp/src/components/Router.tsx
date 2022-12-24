import React from 'react'
import { Routes, Route } from 'react-router-dom'
import Home from '../pages/Home'
import Login from '../pages/Login'
import Brands from '../pages/Brands'
const Countries = React.lazy(() => import('../pages/Countries'));
function Router() {
    return (
        <Routes>
            <Route element={<Home />} path='/' />
            <Route element={<Login />} path='login' />
            <Route element={<Brands />} path='brands' />
            <Route element={<Countries />} path='countries' />
        </Routes>

    )
}

export default Router