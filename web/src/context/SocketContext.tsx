import { HubConnection } from '@microsoft/signalr'
import { createContext } from 'react'

interface SocketContextProps {
	socket?: HubConnection
}

export const SocketContext = createContext<SocketContextProps>({})
