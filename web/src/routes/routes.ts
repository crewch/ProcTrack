import { FC } from 'react'
import Release from '../pages/Release/Release'
import Approval from '../pages/Approval/Approval'

interface Route {
	path: string
	Element: FC
}

export const routes: Route[] = [
	{ path: '/release', Element: Release },
	{ path: '/approval', Element: Approval },
]
