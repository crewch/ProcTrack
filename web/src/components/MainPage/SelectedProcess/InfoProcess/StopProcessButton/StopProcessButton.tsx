import { FC } from 'react'
import { CustomButton } from '../../../../CustomButton/CustomButton'
import { useAppSelector } from '../../../../../hooks/reduxHooks'
import { useMutation, useQueryClient } from '@tanstack/react-query'
import { processService } from '../../../../../services/process'

const StopProcessButton: FC = () => {
	const openedProcessID = useAppSelector(
		state => state.processStage.openedProcess
	)

	const queryClient = useQueryClient()
	const mutation = useMutation({
		mutationKey: [openedProcessID],
		mutationFn: processService.stopProcessId,
		onSuccess: () => {
			queryClient.invalidateQueries({ queryKey: ['processId'] })
			queryClient.invalidateQueries({ queryKey: ['allProcess'] })
			queryClient.invalidateQueries({ queryKey: ['stages'] })
			queryClient.invalidateQueries({ queryKey: ['stageId'] })
		},
	})

	return (
		<CustomButton
			onClick={() => mutation.mutate(openedProcessID)}
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
		</CustomButton>
	)
}

export default StopProcessButton
