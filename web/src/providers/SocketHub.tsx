import { SocketContext } from '@/context/SocketContext'
import { useAppDispatch, useAppSelector } from '@/hooks/reduxHooks'
import { reset } from '@/store/processStageSlice/processStageSlice'
import { getUserData } from '@/utils/getUserData'
import { useQueryClient } from '@tanstack/react-query'
import { FC, ReactNode, useContext, useEffect } from 'react'
import { useLocation } from 'react-router-dom'

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
	const dispatch = useAppDispatch()
	const location = useLocation().pathname

	useEffect(() => {
		if (socket?.state === 'Disconnected') {
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
		socket?.invoke(
			'SetUserConnection',
			JSON.stringify({ UserId: getUserData().id })
		)

		console.log('invoke2')
	})

	useEffect(() => {
		socket?.on('CreateProcessNotification', () => {
			if (location === '/release') {
				queryClient.invalidateQueries({ queryKey: ['allProcess'] })
			}
			console.log('CreateProcessNotification')
		})
	}, [socket])

	useEffect(() => {
		socket?.on(
			'StartProcessNotification',
			({ processId, stageId }: { processId: number; stageId: number }) => {
				if (location === '/release') {
					queryClient.invalidateQueries({ queryKey: ['allProcess'] })
				}

				if (location === '/approval') {
					queryClient.invalidateQueries({ queryKey: ['stagesAllByUserId'] })
				}

				if (openedProcess === processId) {
					if (location === '/release') {
						queryClient.invalidateQueries({
							queryKey: ['processId', processId],
						})

						queryClient.invalidateQueries({ queryKey: ['stages', processId] })
					}
				}

				if (openedStage === stageId) {
					if (location === '/release') {
						queryClient.invalidateQueries({ queryKey: ['stageId', stageId] })
					}
				}

				console.log('StartProcessNotification')
			}
		)
	}, [socket, openedProcess, openedStage])

	useEffect(() => {
		socket?.on(
			'StopProcessNotification',
			({ processId, stageId }: { processId: number; stageId: number }) => {
				if (location === '/release') {
					queryClient.invalidateQueries({ queryKey: ['allProcess'] })
				}

				if (location === '/approval') {
					queryClient.invalidateQueries({ queryKey: ['stagesAllByUserId'] })
				}

				if (openedProcess === processId) {
					if (location === '/release') {
						queryClient.invalidateQueries({
							queryKey: ['processId', processId],
						})
					}

					queryClient.invalidateQueries({ queryKey: ['stages', processId] })
				}

				if (openedStage === stageId) {
					if (location === '/release') {
						queryClient.invalidateQueries({ queryKey: ['stageId', stageId] })
					}

					if (location === '/approval') {
						console.log(location, '<<')

						dispatch(reset())
					}
				}

				console.log('StopProcessNotification')
			}
		)
	}, [socket, openedProcess, openedStage])

	useEffect(() => {
		//BUG не работает
		socket?.on(
			'CreatePassportNotification',
			({ processId }: { processId: number }) => {
				console.log(processId)

				if (openedProcess === processId) {
					queryClient.invalidateQueries({
						queryKey: ['passport', processId],
					})
				}

				console.log('CreatePassportNotification')
			}
		)
	}, [socket, openedProcess])

	//TODO доделать

	socket?.on('UpdateProcessNotification', message => {
		console.log(message)
	})

	socket?.on('AssignStageNotification', message => {
		console.log(message)
	})

	socket?.on('CancelStageNotification', message => {
		console.log(message)
	})

	socket?.on('UpdateStageNotification', message => {
		console.log(message)
	})

	socket?.on('AssignTaskNotification', message => {
		console.log(message)
	})

	socket?.on('StartTaskNotification', message => {
		console.log(message)
	})

	socket?.on('StopTaskNotification', message => {
		console.log(message)
	})

	socket?.on('UpdateEndVerificationNotification', message => {
		console.log(message)
	})

	socket?.on('CreateCommentNotification', message => {
		console.log(message)
	})

	return <>{children}</>
}

export default SocketHub
