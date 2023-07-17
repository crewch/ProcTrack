import { FC } from 'react'
import { CustomButton } from '../../../../CustomButton/CustomButton'
import { useAppSelector } from '../../../../../hooks/reduxHooks'
import { useMutation, useQueryClient } from '@tanstack/react-query'
import { toggleProcessApi } from '../../../../../api/toggleProcessApi'

const StartProcessButton: FC = () => {
	const openedProcessID = useAppSelector(state => state.processes.openedProcess)

	const queryClient = useQueryClient()
	const mutation = useMutation({
		mutationKey: [openedProcessID],
		mutationFn: toggleProcessApi.startProcessId,
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
			endIcon={<img src='/playProcess.svg' height='20px' width='20px' />}
		>
			Начать процесс
		</CustomButton>
	)
}

export default StartProcessButton
