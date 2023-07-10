import { FC } from 'react'
import ListTasks from '../../../MainPage/SelectedStage/ListTasks/ListTasks'
import { ISelectedStageChildProps } from '../../../../interfaces/IStageForSuccessPage/ISelectedStage/ISelectedStage'
import { LinearProgress } from '@mui/material'

const Footer: FC<ISelectedStageChildProps> = ({
	selectedStage,
	isLoading,
	isSuccess,
}) => {
	return (
		<>
			{isLoading && <LinearProgress />}
			{isSuccess && selectedStage && (
				<ListTasks group={selectedStage.holds[0].groups[0].title} />
			)}
		</>
	)
}

export default Footer
