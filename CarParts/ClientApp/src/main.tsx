import ReactDOM from 'react-dom/client'
import App from './App'
import './index.scss'
import { store } from './store'
import { Provider } from 'react-redux'
ReactDOM.createRoot(document.getElementById('root') as HTMLElement).render(

  <Provider store={store}>



    <App />

  </Provider>
)
