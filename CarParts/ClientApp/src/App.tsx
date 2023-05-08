import React, { PropsWithChildren, Suspense, useContext, useEffect } from 'react';
// import { fetchBrands } from '@/store/brands';
import Router from './components/Router';
import CssBaseline from '@mui/material/CssBaseline';
import { BrowserRouter } from 'react-router-dom'
import { customTheme } from './theme';
import rtlPlugin from 'stylis-plugin-rtl';
import { CacheProvider } from '@emotion/react';
import createCache from '@emotion/cache';
import { prefixer } from 'stylis';
import { ThemeProvider } from '@mui/material';
import { useDispatch } from 'react-redux';
import { AppDispatch } from './store';
import { QueryClient, QueryClientProvider, useQuery } from 'react-query'
import { ReactQueryDevtools } from 'react-query/devtools'
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { BrandApi } from './api/Brand';
import { BrandsActions } from './store/brands';
import { BrandItem } from './api/Brand/dto';
import { CountryItem } from './api/Country/dto';
import { CountryActions } from './store/countries';
import { CountryApi } from './api/Country';
import { SERVER_URL } from '../app.config';
import { AdapterMoment } from '@mui/x-date-pickers/AdapterMoment';
import { LocalizationProvider } from '@mui/x-date-pickers';

const stylisPlugins = [prefixer];
const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      // refetchOnWindowFocus: false
    }
  }
})
const htmlDir = document.querySelector('html');
if (htmlDir?.dir === 'rtl') {
  stylisPlugins.push(rtlPlugin)
}

const cacheRtl = createCache({
  key: 'muirtl',
  stylisPlugins,

});
function RTL(props: any) {
  return <CacheProvider value={cacheRtl}>{props.children}</CacheProvider>;
}
const StartupCalls = (props: PropsWithChildren) => {
  const dispatch = useDispatch<AppDispatch>()

  const brandQuery = useQuery<BrandItem[]>('brand', BrandApi.fetchBrands, {
    onSuccess: (data) => dispatch(BrandsActions.SetCars(data))
  })

  const countryQuery = useQuery<CountryItem[]>('country', CountryApi.fetchCountries, {
    onSuccess: (data) => dispatch(CountryActions.SetCountries(data))
  })

  return <> {props.children} </>
}
function App() {



  return (
    <div className="app tw-w-full">
      <ThemeProvider theme={customTheme}>
        <LocalizationProvider dateAdapter={AdapterMoment}>

          <QueryClientProvider client={queryClient}>
            <StartupCalls />
            <RTL>

              <CssBaseline />
              <BrowserRouter>



                <Suspense fallback={'Loading Some Thing'}>

                  <main className='tw-block'>
                    <Router />

                  </main>
                </Suspense>
              </BrowserRouter>
            </RTL>
            <ReactQueryDevtools />
            <ToastContainer />

          </QueryClientProvider>
        </LocalizationProvider>
      </ThemeProvider>
      <div className="fixed bottom-4 right-4  z-[10000]  text-white">
        <a href={`${SERVER_URL}/swagger/index.html`} target='_blank'>Swagger Api</a>
      </div>
    </div>
  );
}

export default App;
