import { SocketContext } from '@/context/SocketContext'
import { getUserData } from '@/utils/getUserData'
import { useQueryClient } from '@tanstack/react-query'
import { FC, ReactNode, useContext, useEffect } from 'react'

interface SocketHubProps {
	children: ReactNode
}

const SocketHub: FC<SocketHubProps> = ({ children }) => {
	const queryClient = useQueryClient()
	const { socket } = useContext(SocketContext)

	if (socket) {
		useEffect(() => {
			if (socket.state === 'Disconnected') {
				socket.start().then(() => {
					if (getUserData()) {
						socket.invoke(
							'SetUserConnection',
							JSON.stringify({ UserId: getUserData().id })
						)
						console.log('invoke1')
					}
				})
			}
		}, [socket])

		addEventListener('localStorageChange', () => {
			socket.invoke(
				'SetUserConnection',
				JSON.stringify({ UserId: getUserData().id })
			)

			console.log('invoke2')
		})

		socket.on('CreateProcessNotification', () => {
			queryClient.invalidateQueries({ queryKey: ['allProcess'] })
			console.log('allProcess')
		})

		socket.on('UpdateProcessNotification', message => {
			console.log(message)
		})

		socket.on('StartProcessNotification', message => {
			console.log(message)
		})

		socket.on('StopProcessNotification', message => {
			console.log(message)
		})

		socket.on('CreatePassportNotification', message => {
			console.log(message)
		})

		socket.on('AssignStageNotification', message => {
			console.log(message)
		})

		socket.on('CancelStageNotification', message => {
			console.log(message)
		})

		socket.on('UpdateStageNotification', message => {
			console.log(message)
		})

		socket.on('AssignTaskNotification', message => {
			console.log(message)
		})

		socket.on('StartTaskNotification', message => {
			console.log(message)
		})

		socket.on('StopTaskNotification', message => {
			console.log(message)
		})

		socket.on('UpdateEndVerificationNotification', message => {
			console.log(message)
		})

		socket.on('CreateCommentNotification', message => {
			console.log(message)
		})
	}

	return <>{children}</>
}

export default SocketHub
