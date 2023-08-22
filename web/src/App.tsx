import './index.scss'
import '@fontsource/roboto/300.css'
import '@fontsource/roboto/400.css'
import '@fontsource/roboto/500.css'
import '@fontsource/roboto/700.css'
import '@fontsource/montserrat/300.css'
import '@fontsource/montserrat/400.css'
import '@fontsource/montserrat/500.css'
import '@fontsource/montserrat/600.css'
import '@fontsource/montserrat/700.css'
import { BrowserRouter, Navigate, Route, Routes } from 'react-router-dom'
import { routes } from './routes/routes'
import { QueryClient, QueryClientProvider } from '@tanstack/react-query'
import { Provider } from 'react-redux'
import { store } from './store/store.ts'
import Layout from './pages/Layout/Layout.tsx'
import NotFound from './pages/NotFound/NotFound.tsx'
import LoginPage from './pages/Login/Login.tsx'
import * as signalR from '@microsoft/signalr'
import { SocketContext } from './context/SocketContext.tsx'
import SocketHub from './providers/SocketHub.tsx'
import { HOST } from './configs/url.ts'

const queryClient = new QueryClient({
	defaultOptions: { queries: { refetchOnWindowFocus: false } },
})

const socket = new signalR.HubConnectionBuilder()
	.withUrl(`http://${HOST}:8001/notifications`)
	.build()

const App = () => {
	return (
		<QueryClientProvider client={queryClient}>
			<SocketContext.Provider value={{ socket }}>
				<Provider store={store}>
					<SocketHub>
						<BrowserRouter>
							<Routes>
								<Route path='/' element={<Layout />}>
									{routes.map(({ path, Element }, index) => (
										<Route path={path} element={<Element />} key={index} />
									))}
								</Route>
								<Route path='login' element={<LoginPage />} />
								<Route path='404' element={<NotFound />} />
								<Route path='*' element={<Navigate to='404' />} />
							</Routes>
						</BrowserRouter>
					</SocketHub>
				</Provider>
			</SocketContext.Provider>
		</QueryClientProvider>
	)
}

export default App
