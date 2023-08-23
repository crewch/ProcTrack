import { SocketContext } from '@/context/SocketContext'
import { useAppDispatch, useAppSelector } from '@/hooks/reduxHooks'
import { reset } from '@/store/processStageSlice/processStageSlice'
import { getPage } from '@/utils/getPage'
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

	useEffect(() => {
		const handleLocalStorageChange = () => {
			socket?.invoke(
				'SetUserConnection',
				JSON.stringify({ UserId: getUserData().id })
			)

			console.log('invoke2')
		}

		addEventListener('localStorageChange', handleLocalStorageChange)

		return () => {
			removeEventListener('localStorageChange', handleLocalStorageChange)
		}
	}, [socket])

	useEffect(() => {
		const handleCreateProcessNotification = () => {
			if (getPage() === 'release') {
				queryClient.invalidateQueries({ queryKey: ['allProcess'] })
			}
		}

		socket?.on('CreateProcessNotification', handleCreateProcessNotification)

		return () => {
			socket?.off('CreateProcessNotification', handleCreateProcessNotification)
		}
	}, [queryClient, socket])

	useEffect(() => {
		const handleStartProcessNotification = ({
			processId,
			stageId,
		}: {
			processId: number
			stageId: number
		}) => {
			if (getPage() === 'release') {
				queryClient.invalidateQueries({ queryKey: ['allProcess'] })
			}

			if (getPage() === 'approval') {
				queryClient.invalidateQueries({ queryKey: ['stagesAllByUserId'] })
			}

			if (openedProcess === processId) {
				if (getPage() === 'release') {
					queryClient.invalidateQueries({
						queryKey: ['processId', processId],
					})
					queryClient.invalidateQueries({ queryKey: ['stages', processId] })
				}
			}

			if (openedStage === stageId) {
				if (getPage() === 'release') {
					queryClient.invalidateQueries({ queryKey: ['stageId', stageId] })
				}
			}
		}

		socket?.on('StartProcessNotification', handleStartProcessNotification)

		return () => {
			socket?.off('StartProcessNotification', handleStartProcessNotification)
		}
	}, [openedProcess, openedStage, queryClient, socket])

	useEffect(() => {
		const handleStopProcessNotification = ({
			processId,
			stageId,
		}: {
			processId: number
			stageId: number
		}) => {
			if (getPage() === 'release') {
				queryClient.invalidateQueries({ queryKey: ['allProcess'] })
			}

			if (getPage() === 'approval') {
				queryClient.invalidateQueries({ queryKey: ['stagesAllByUserId'] })
			}

			if (openedProcess === processId) {
				if (getPage() === 'release') {
					queryClient.invalidateQueries({
						queryKey: ['processId', processId],
					})
				}

				queryClient.invalidateQueries({ queryKey: ['stages', processId] })
			}

			if (openedStage === stageId) {
				if (getPage() === 'release') {
					queryClient.invalidateQueries({ queryKey: ['stageId', stageId] })
				}

				if (getPage() === 'approval') {
					dispatch(reset())
				}
			}
		}

		socket?.on('StopProcessNotification', handleStopProcessNotification)

		return () => {
			socket?.off('StopProcessNotification', handleStopProcessNotification)
		}
	}, [openedProcess, openedStage, queryClient, socket, dispatch])

	useEffect(() => {
		const handleCreatePassportNotification = ({
			processId,
		}: {
			processId: number
		}) => {
			if (openedProcess === processId) {
				queryClient.invalidateQueries({
					queryKey: ['passport', processId],
				})
			}
		}

		socket?.on('CreatePassportNotification', handleCreatePassportNotification)

		return () => {
			socket?.off(
				'CreatePassportNotification',
				handleCreatePassportNotification
			)
		}
	}, [openedProcess, queryClient, socket])

	useEffect(() => {
		const handleCancelStageNotification = ({
			processId,
			stageId,
		}: {
			processId: number
			stageId: number
		}) => {
			if (stageId === openedStage) {
				queryClient.invalidateQueries({
					queryKey: ['stageId', stageId],
				})
			}

			if (getPage() === 'release' && openedProcess === processId) {
				queryClient.invalidateQueries({
					queryKey: ['processId', processId],
				})

				queryClient.invalidateQueries({
					queryKey: ['allProcess'],
				})
			}

			if (getPage() === 'approval') {
				queryClient.invalidateQueries({
					queryKey: ['stagesAllByUserId'],
				})

				queryClient.invalidateQueries({
					queryKey: ['processByStageId', stageId],
				})
			}

			queryClient.invalidateQueries({ queryKey: ['stages', processId] })
		}

		socket?.on('CancelStageNotification', handleCancelStageNotification)

		return () => {
			socket?.off('CancelStageNotification', handleCancelStageNotification)
		}
	}, [openedStage, openedProcess, queryClient, socket])

	useEffect(() => {
		const handleUpdateStageNotification = ({
			processId,
		}: {
			processId: number
		}) => {
			if (getPage() === 'approval') {
				queryClient.invalidateQueries({
					queryKey: ['stagesAllByUserId'],
				})
			}

			if (openedProcess === processId) {
				queryClient.invalidateQueries({ queryKey: ['stages', processId] })
			}
		}

		socket?.on('UpdateStageNotification', handleUpdateStageNotification)

		return () => {
			socket?.off('UpdateStageNotification', handleUpdateStageNotification)
		}
	}, [openedProcess, queryClient, socket])

	useEffect(() => {
		const handleAssignTaskNotification = ({ stageId }: { stageId: number }) => {
			if (getPage() === 'approval') {
				queryClient.invalidateQueries({ queryKey: ['stagesAllByUserId'] })
			}

			if (openedStage === stageId) {
				// BUG не приходят данные

				queryClient.invalidateQueries({
					queryKey: ['stageId', stageId],
				})

				queryClient.invalidateQueries({
					queryKey: ['tasks', stageId],
				})
			}
		}

		socket?.on('AssignTaskNotification', handleAssignTaskNotification)

		return () => {
			socket?.off('AssignTaskNotification', handleAssignTaskNotification)
		}
	}, [openedStage, queryClient, socket])

	useEffect(() => {
		const handleStartTaskNotification = ({ stageId }: { stageId: number }) => {
			if (getPage() === 'approval') {
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

		socket?.on('StartTaskNotification', handleStartTaskNotification)

		return () => {
			socket?.off('StartTaskNotification', handleStartTaskNotification)
		}
	}, [openedStage, queryClient, socket])

	useEffect(() => {
		const handleStopTaskNotification = ({ stageId }: { stageId: number }) => {
			if (getPage() === 'approval') {
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

		socket?.on('StopTaskNotification', handleStopTaskNotification)

		return () => {
			socket?.off('StopTaskNotification', handleStopTaskNotification)
		}
	}, [openedStage, queryClient, socket])

	useEffect(() => {
		const handleUpdateEndVerificationNotification = ({
			stageId,
		}: {
			stageId: number
		}) => {
			if (getPage() === 'approval') {
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

		socket?.on(
			'UpdateEndVerificationNotification',
			handleUpdateEndVerificationNotification
		)

		return () => {
			socket?.off(
				'UpdateEndVerificationNotification',
				handleUpdateEndVerificationNotification
			)
		}
	}, [openedStage, queryClient, socket])

	useEffect(() => {
		const handleCreateCommentNotification = ({
			stageId,
		}: {
			stageId: number
		}) => {
			if (openedStage === stageId) {
				queryClient.invalidateQueries({ queryKey: ['tasks'] })
			}
		}

		socket?.on('CreateCommentNotification', handleCreateCommentNotification)

		return () =>
			socket?.off('CreateCommentNotification', handleCreateCommentNotification)
	}, [openedStage, queryClient, socket])

	useEffect(() => {
		socket?.on('UpdateProcessNotification', () => {
			//TODO доделать
		})
	}, [socket])

	useEffect(() => {
		const handleAssignStageNotification = ({
			processId,
			stageId,
		}: {
			processId: number
			stageId: number
		}) => {
			if (stageId === openedStage) {
				queryClient.invalidateQueries({
					queryKey: ['stageId', stageId],
				})
			}

			if (getPage() === 'release' && openedProcess === processId) {
				queryClient.invalidateQueries({
					queryKey: ['processId', processId],
				})

				queryClient.invalidateQueries({
					queryKey: ['allProcess'],
				})
			}

			if (getPage() === 'approval') {
				queryClient.invalidateQueries({
					queryKey: ['stagesAllByUserId'],
				})

				queryClient.invalidateQueries({
					queryKey: ['processByStageId', stageId],
				})
			}

			queryClient.invalidateQueries({ queryKey: ['stages', processId] })
		}

		socket?.on('AssignStageNotification', handleAssignStageNotification)

		return () =>
			socket?.off('AssignStageNotification', handleAssignStageNotification)
	}, [openedProcess, openedStage, socket, queryClient])

	return <>{children}</>
}

export default SocketHub
