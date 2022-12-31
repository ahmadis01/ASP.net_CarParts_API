import React, { Suspense, useContext, useEffect } from 'react';
import { fetchBrands } from '@/store/brands';
import Router from './components/Router';
import CssBaseline from '@mui/material/CssBaseline';
import { BrowserRouter } from 'react-router-dom'
import { customTheme } from './theme';
import rtlPlugin from 'stylis-plugin-rtl';
import { CacheProvider } from '@emotion/react';
import createCache from '@emotion/cache';
import { prefixer } from 'stylis';
import Layout from './components/Layout'
import { ThemeProvider } from '@mui/material';
import { useDispatch } from 'react-redux';
import { AppDispatch } from './store';
import { fetchCountries } from './store/countries';
const stylisPlugins = [prefixer];
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
function App() {
  const dispatch = useDispatch<AppDispatch>()

  useEffect(() => {
    dispatch(fetchBrands());
    dispatch(fetchCountries());
  }, [])

  return (
    <div className="app tw-w-full">
      <ThemeProvider theme={customTheme}>
        <RTL>

          <CssBaseline />
          <BrowserRouter>



            <Layout>
              <Suspense fallback={'Loading Some Thing'}>

                <main className='tw-block'>
                  <Router />

                </main>
              </Suspense>
            </Layout>
          </BrowserRouter>
        </RTL>
      </ThemeProvider>
    </div>
  );
}

export default App;
