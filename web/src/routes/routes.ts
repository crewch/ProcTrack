import { FC } from 'react'
import Release from '@/pages/Release/Release'
import Approval from '@/pages/Approval/Approval'

interface ElementProps {
	socket: signalR.HubConnection
}

interface Route {
	path: string
	Element: FC<ElementProps>
}

export const routes: Route[] = [
	{ path: '/release', Element: Release },
	{ path: '/approval', Element: Approval },
]
