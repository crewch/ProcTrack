import { Box, LinearProgress } from '@mui/material'
import DateInfo from '../../../MainPage/SelectedProcess/InfoProcess/DateInfoField/DateInfo'
import styles from '/src/styles/StageForSuccessPageStyles/SelectedProcessStyles/FooterStyles/Footer.module.scss'
import { FC } from 'react'
import { ISelectedStageChildProps } from '../../../../interfaces/IStageForSuccessPage/ISelectedStage/ISelectedStage'

const Body: FC<ISelectedStageChildProps> = ({
	selectedStage,
	isLoading,
	isSuccess,
}) => {
	return (
		<Box className={styles.container}>
			{isLoading && <LinearProgress />}
			{isSuccess && selectedStage && (
				<DateInfo
					page='stageForSuccess'
					startDate={selectedStage.createdAt}
					success={
						selectedStage.approvedAt
							? selectedStage.approvedAt
							: 'Ещё не согласован'
					}
					confirm={
						selectedStage.signedAt
							? selectedStage.signedAt
							: 'Ещё не согласован'
					}
				/>
			)}
		</Box>
	)
}

export default Body
