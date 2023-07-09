import MainPage from '../pages/MainPage'
import { IRoutes } from '../interfaces/IRoutes/IRoutes'
import StageForSuccessPage from '../pages/StageForSuccessPage'

export const routes: IRoutes[] = [
	{ path: '/', Element: MainPage },
	{ path: '/stageforsuccess', Element: StageForSuccessPage },
]
