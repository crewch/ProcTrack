import { FC } from 'react'
import { GrayButton } from '../../../../ui/button/GrayButton'
import { useAppSelector } from '../../../../../hooks/reduxHooks'
import { useMutation, useQueryClient } from '@tanstack/react-query'
import { processService } from '../../../../../services/process'

const StartProcessButton: FC = () => {
	const openedProcessID = useAppSelector(
		state => state.processStage.openedProcess
	)

	const queryClient = useQueryClient()
	const mutation = useMutation({
		mutationKey: [openedProcessID],
		mutationFn: processService.startProcessId,
		onSuccess: () => {
			queryClient.invalidateQueries({ queryKey: ['processId'] })
			queryClient.invalidateQueries({ queryKey: ['allProcess'] })
			queryClient.invalidateQueries({ queryKey: ['stages'] })
			queryClient.invalidateQueries({ queryKey: ['stageId'] })
		},
	})

	return (
		<GrayButton
			onClick={() => mutation.mutate(openedProcessID)}
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
	)
}

export default StartProcessButton
