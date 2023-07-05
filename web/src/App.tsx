import { Route, Routes } from 'react-router-dom'
import { routes } from './routes/routes'
import Layout from './pages/Layout'
import NotFoundPage from './pages/NotFoundPage'
import LoginPage from './pages/LoginPage'

const App = () => {
	return (
		<Routes>
			<Route path='*' element={<NotFoundPage />} />
			<Route path='login' element={<LoginPage />} />
			<Route path='/' element={<Layout />}>
				{routes.map(({ path, Element }, index) => (
					<Route path={path} element={<Element />} key={index} />
				))}
			</Route>
		</Routes>
	)
}

export default App
