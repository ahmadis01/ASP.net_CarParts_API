import React, { Suspense } from 'react'
import { Routes, Route } from 'react-router-dom'
import Dashboard from '@/components/layouts/Dashboard'
import FullScreen from '@/components/layouts/FullScreen'
import Home from '../pages/Home'
import Login from '../pages/Login'
import Brands from '../pages/Brands'
import Cars from '../pages/Cars'
import Parts from '../pages/Parts';
import Invoces from '../pages/Invoces';
import Accounting from '../pages/Accounting';
import Settings from '../pages/Settings';
import Inventories from '../pages/Inventories';
import Clients from '../pages/Clients';

const Countries = React.lazy(() => import('../pages/Countries'));
function Router() {
    const routes = [
        {
            layout: Dashboard,
            name: Home,
            path: '/'
        },
        {
            layout: Dashboard,
            name: Brands,
            path: '/brands'
        },
        {
            layout: Dashboard,
            name: Cars,
            path: '/cars'
        },
        {
            layout: Dashboard,
            name: Countries,
            path: '/countries'
        },
        {
            layout: Dashboard,
            name: Parts,
            path: '/products'
        },
        {
            layout: Dashboard,
            name: Invoces,
            path: '/invoces'
        },
        {
            layout: Dashboard,
            name: Accounting,
            path: '/accounting'
        },
        {
            layout: Dashboard,
            name: Settings,
            path: '/settings'
        },
        {
            layout: Dashboard,
            name: Inventories,
            path: '/inventories'
        },
        {
            layout: Dashboard,
            name: Clients,
            path: '/clients'
        },
        {
            layout: FullScreen,
            name: Login,
            path: '/login'
        },

    ]

    return (
        <Routes>
            {routes.map((Ele, i) => (
                <Route key={i} element={
                    <Ele.layout>
                        <Suspense fallback={'Loading Some Thing'}>
                            <main >
                                <Ele.name />

                            </main>
                        </Suspense>
                    </Ele.layout>
                } path={Ele.path} >
                </Route>
            ))}
        </Routes>
    )
}

export default Router