import { useQueryClient, useMutation } from '@tanstack/react-query'
import { useAppSelector } from '../../../../../hooks/reduxHooks'
import { processService } from '../../../../../services/process'
import { GrayButton } from '../../../../ui/button/GrayButton'
import { FC } from 'react'

interface ToggleProcessButtonProps {
	status: 'в процессе' | 'завершен' | 'остановлен' | 'отменен'
}

const ToggleProcessButton: FC<ToggleProcessButtonProps> = ({ status }) => {
	const openedProcessID = useAppSelector(
		state => state.processStage.openedProcess
	)

	const queryClient = useQueryClient()

	const mutationStartProcess = useMutation({
		mutationFn: processService.startProcessId,
		onSuccess: () => {
			queryClient.invalidateQueries({ queryKey: ['processId'] })
			queryClient.invalidateQueries({ queryKey: ['allProcess'] })
			queryClient.invalidateQueries({ queryKey: ['stages'] })
			queryClient.invalidateQueries({ queryKey: ['stageId'] })
		},
	})

	const mutationStopProcess = useMutation({
		mutationFn: processService.stopProcessId,
		onSuccess: () => {
			queryClient.invalidateQueries({ queryKey: ['processId'] })
			queryClient.invalidateQueries({ queryKey: ['allProcess'] })
			queryClient.invalidateQueries({ queryKey: ['stages'] })
			queryClient.invalidateQueries({ queryKey: ['stageId'] })
		},
	})

	return (
		<>
			{status === 'в процессе' && (
				<GrayButton
					onClick={() => mutationStopProcess.mutate(openedProcessID)}
					sx={{
						fontSize: {
							xs: '12px',
							lg: '14px',
						},
					}}
					variant='contained'
					endIcon={<img src='/pause.svg' height='20px' width='20px' />}
				>
					Остановить процесс
				</GrayButton>
			)}
			{status === 'остановлен' && (
				<GrayButton
					onClick={() => mutationStartProcess.mutate(openedProcessID)}
					sx={{
						fontSize: {
							xs: '12px',
							lg: '14px',
						},
					}}
					variant='contained'
					endIcon={<img src='/playProcess.svg' height='20px' width='20px' />}
				>
					Начать процесс
				</GrayButton>
			)}
		</>
	)
}

export default ToggleProcessButton
