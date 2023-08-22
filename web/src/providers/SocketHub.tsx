import { SocketContext } from '@/context/SocketContext'
import { useAppDispatch, useAppSelector } from '@/hooks/reduxHooks'
import { reset } from '@/store/processStageSlice/processStageSlice'
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
	const dispatch = useAppDispatch()

	function getUrl() {
		return window.location.pathname.slice(1)
	}

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
	}, [])

	addEventListener('localStorageChange', () => {
		socket?.invoke(
			'SetUserConnection',
			JSON.stringify({ UserId: getUserData().id })
		)

		console.log('invoke2')
	})

	useEffect(() => {
		socket?.on('CreateProcessNotification', () => {
			if (getUrl() === 'release') {
				queryClient.invalidateQueries({ queryKey: ['allProcess'] })
			}
		})
	}, [])

	useEffect(() => {
		socket?.on(
			'StartProcessNotification',
			({ processId, stageId }: { processId: number; stageId: number }) => {
				if (getUrl() === 'release') {
					queryClient.invalidateQueries({ queryKey: ['allProcess'] })
				}

				if (getUrl() === 'approval') {
					queryClient.invalidateQueries({ queryKey: ['stagesAllByUserId'] })
				}

				if (openedProcess === processId) {
					if (getUrl() === 'release') {
						queryClient.invalidateQueries({
							queryKey: ['processId', processId],
						})

						queryClient.invalidateQueries({ queryKey: ['stages', processId] })
					}
				}

				if (openedStage === stageId) {
					if (getUrl() === 'release') {
						queryClient.invalidateQueries({ queryKey: ['stageId', stageId] })
					}
				}
			}
		)
	}, [openedProcess, openedStage])

	useEffect(() => {
		socket?.on(
			'StopProcessNotification',
			({ processId, stageId }: { processId: number; stageId: number }) => {
				if (getUrl() === 'release') {
					queryClient.invalidateQueries({ queryKey: ['allProcess'] })
				}

				if (getUrl() === 'approval') {
					queryClient.invalidateQueries({ queryKey: ['stagesAllByUserId'] })
				}

				if (openedProcess === processId) {
					if (getUrl() === 'release') {
						queryClient.invalidateQueries({
							queryKey: ['processId', processId],
						})
					}

					queryClient.invalidateQueries({ queryKey: ['stages', processId] })
				}

				if (openedStage === stageId) {
					if (getUrl() === 'release') {
						queryClient.invalidateQueries({ queryKey: ['stageId', stageId] })
					}

					if (getUrl() === 'approval') {
						dispatch(reset())
					}
				}
			}
		)
	}, [openedProcess, openedStage])

	useEffect(() => {
		socket?.on(
			'CreatePassportNotification',
			({ processId }: { processId: number }) => {
				if (openedProcess === processId) {
					queryClient.invalidateQueries({
						queryKey: ['passport', processId],
					})
				}
			}
		)
	}, [openedProcess])

	useEffect(() => {
		socket?.on(
			'CancelStageNotification',
			({ processId, stageId }: { processId: number; stageId: number }) => {
				if (stageId === openedStage) {
					queryClient.invalidateQueries({
						queryKey: ['stageId', stageId],
					})
				}

				if (getUrl() === 'release' && openedProcess === processId) {
					queryClient.invalidateQueries({
						queryKey: ['processId', processId],
					})

					queryClient.invalidateQueries({
						queryKey: ['allProcess'],
					})
				}

				if (getUrl() === 'approval') {
					queryClient.invalidateQueries({
						queryKey: ['stagesAllByUserId'],
					})

					queryClient.invalidateQueries({
						queryKey: ['processByStageId', stageId],
					})
				}

				queryClient.invalidateQueries({ queryKey: ['stages', processId] })
			}
		)
	}, [openedStage, openedProcess])

	useEffect(() => {
		socket?.on(
			'UpdateStageNotification',
			({ processId }: { processId: number }) => {
				if (getUrl() === 'approval') {
					queryClient.invalidateQueries({
						queryKey: ['stagesAllByUserId'],
					})
				}

				if (openedProcess === processId) {
					queryClient.invalidateQueries({ queryKey: ['stages', processId] })
				}
			}
		)
	}, [openedProcess])

	useEffect(() => {
		socket?.on('AssignTaskNotification', ({ stageId }: { stageId: number }) => {
			if (getUrl() === 'approval') {
				queryClient.invalidateQueries({ queryKey: ['stagesAllByUserId'] })
			}

			if (openedStage === stageId) {
				queryClient.invalidateQueries({
					queryKey: ['stageId', stageId],
				})

				queryClient.invalidateQueries({
					queryKey: ['tasks', stageId],
				})
			}
		})
	}, [openedStage])

	useEffect(() => {
		socket?.on('StartTaskNotification', ({ stageId }: { stageId: number }) => {
			if (getUrl() === 'approval') {
				queryClient.invalidateQueries({ queryKey: ['stagesAllByUserId'] })
			}

			if (openedStage === stageId) {
				queryClient.invalidateQueries({
					queryKey: ['stageId', stageId],
				})

				queryClient.invalidateQueries({
					queryKey: ['tasks', stageId],
				})
			}
		})
	}, [openedStage])

	useEffect(() => {
		socket?.on('StopTaskNotification', ({ stageId }: { stageId: number }) => {
			if (getUrl() === 'approval') {
				queryClient.invalidateQueries({ queryKey: ['stagesAllByUserId'] })
			}

			if (openedStage === stageId) {
				queryClient.invalidateQueries({
					queryKey: ['stageId', stageId],
				})

				queryClient.invalidateQueries({
					queryKey: ['tasks', stageId],
				})
			}
		})
	}, [openedStage])

	useEffect(() => {
		socket?.on(
			'UpdateEndVerificationNotification',
			({ stageId }: { stageId: number }) => {
				if (getUrl() === 'approval') {
					queryClient.invalidateQueries({ queryKey: ['stagesAllByUserId'] })
				}

				if (openedStage === stageId) {
					queryClient.invalidateQueries({
						queryKey: ['stageId', stageId],
					})

					queryClient.invalidateQueries({
						queryKey: ['tasks', stageId],
					})
				}
			}
		)
	}, [openedStage])

	useEffect(() => {
		socket?.on(
			'CreateCommentNotification',
			({ stageId }: { stageId: number }) => {
				if (openedStage === stageId) {
					queryClient.invalidateQueries({ queryKey: ['tasks'] })
				}
			}
		)
	}, [openedStage])

	useEffect(() => {
		socket?.on('UpdateProcessNotification', message => {
			//TODO доделать
			console.log(message)
		})
	}, [])

	useEffect(() => {
		socket?.on('AssignStageNotification', message => {
			//TODO доделать
			console.log(message)
		})
	}, [])

	return <>{children}</>
}

export default SocketHub
