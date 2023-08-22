import { SocketContext } from '@/context/SocketContext'
import { useAppSelector } from '@/hooks/reduxHooks'
import { getUserData } from '@/utils/getUserData'
import { useQueryClient } from '@tanstack/react-query'
import { FC, ReactNode, useContext, useEffect } from 'react'

interface SocketHubProps {
	children: ReactNode
}

const SocketHub: FC<SocketHubProps> = ({ children }) => {
	const queryClient = useQueryClient()
	const { socket } = useContext(SocketContext)
	const openedProcess = useAppSelector(
		state => state.processStage.openedProcess
	)
	const openedStage = useAppSelector(state => state.processStage.openedStage)

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

			socket.on('CreateProcessNotification', () => {
				queryClient.invalidateQueries({ queryKey: ['allProcess'] })
				console.log('CreateProcessNotification')
			})

			socket.on('UpdateProcessNotification', message => {
				console.log(message)
				//TODO доделать
			})
		}, [socket])

		useEffect(() => {
			socket.on(
				'StartProcessNotification',
				({ processId, stageId }: { processId: number; stageId: number }) => {
					queryClient.invalidateQueries({ queryKey: ['allProcess'] })

					if (openedProcess === processId) {
						console.log(openedProcess, processId)

						queryClient.invalidateQueries({
							queryKey: ['processId', processId],
						})
						queryClient.invalidateQueries({ queryKey: ['stages', processId] })
					}

					if (openedStage === stageId) {
						console.log(openedStage, stageId)

						queryClient.invalidateQueries({ queryKey: ['stageId'] })
					}

					console.log('StartProcessNotification')
				}
			)

			socket.on(
				'StopProcessNotification',
				({ processId, stageId }: { processId: number; stageId: number }) => {
					queryClient.invalidateQueries({ queryKey: ['allProcess'] })

					if (openedProcess === processId) {
						queryClient.invalidateQueries({
							queryKey: ['processId', processId],
						})
						queryClient.invalidateQueries({ queryKey: ['stages', processId] })
					}

					if (openedStage === stageId) {
						queryClient.invalidateQueries({ queryKey: ['stageId'] })
					}

					console.log('StopProcessNotification')
				}
			)
		}, [socket, openedProcess, openedStage])

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

		addEventListener('localStorageChange', () => {
			socket.invoke(
				'SetUserConnection',
				JSON.stringify({ UserId: getUserData().id })
			)

			console.log('invoke2')
		})
	}

	return <>{children}</>
}

export default SocketHub
