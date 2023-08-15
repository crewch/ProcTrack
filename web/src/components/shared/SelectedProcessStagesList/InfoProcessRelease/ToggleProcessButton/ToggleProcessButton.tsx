import { useQueryClient, useMutation } from '@tanstack/react-query'
import { FC } from 'react'
import { GrayButton } from '@/components/ui/button/GrayButton'
import { useAppSelector } from '@/hooks/reduxHooks'
import { processService } from '@/services/process'
import styles from './ToggleProcessButton.module.scss'

interface ToggleProcessButtonProps {
	status: 'в процессе' | 'завершен' | 'остановлен' | 'в доработке'
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
							xs: '0.75rem',
							lg: '0.875rem',
						},
					}}
					variant='contained'
					endIcon={<img src='/pause.svg' className={styles.grayButtonImg} />}
				>
					Остановить процесс
				</GrayButton>
			)}
			{status === 'остановлен' && (
				<GrayButton
					onClick={() => mutationStartProcess.mutate(openedProcessID)}
					sx={{
						fontSize: {
							xs: '0.75rem',
							lg: '0.875rem',
						},
					}}
					variant='contained'
					endIcon={
						<img src='/playProcess.svg' className={styles.grayButtonImg} />
					}
				>
					Начать процесс
				</GrayButton>
			)}
		</>
	)
}

export default ToggleProcessButton
