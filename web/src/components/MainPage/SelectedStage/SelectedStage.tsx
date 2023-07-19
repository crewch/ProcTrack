import { Box, Divider, LinearProgress } from '@mui/material'
import UserField from '../SelectedProcess/InfoProcess/UserField/UserField'
import DateInfo from '../SelectedProcess/InfoProcess/DateInfoField/DateInfo'
import HeaderField from './HeaderField/HeaderField'
import ListTasks from './ListTasks/ListTasks'
import { useQuery } from '@tanstack/react-query'
import { useAppSelector } from '../../../hooks/reduxHooks'
import { FC } from 'react'
import Buttons from '../../StageForSuccessPage/SelectedProcessAndListStage/SelectedStage/Buttons/Buttons'
import { stageService } from '../../../services/stage'
import styles from '/src/styles/MainPageStyles/SelectedStageStyles/SelectedStage.module.scss'

interface SelectedStageProps {
	page?: 'main' | 'stageForSuccess'
}

const SelectedStage: FC<SelectedStageProps> = ({ page }) => {
	const openedStageID = useAppSelector(state => state.processStage.openedStage)

	const {
		data: selectedStage,
		isLoading,
		isSuccess,
	} = useQuery({
		queryKey: ['stageId', openedStageID],
		queryFn: () => stageService.getStageId(openedStageID),
	})

	return (
		<Box className={styles.selectedStage}>
			{isLoading && <LinearProgress />}
			{isSuccess && selectedStage && (
				<>
					<HeaderField
						name={selectedStage.title}
						status={selectedStage.status}
						nameOfGroup={
							selectedStage?.holds[0]?.groups[0]?.title ||
							selectedStage?.holds[1]?.groups[0]?.title
						} //TODO: тут проблема при статусе ещё не начат
						page={page}
					/>
					<Divider className={styles.divider} />
					<DateInfo
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
					<Divider className={styles.divider} />
					<UserField
						responsible={
							selectedStage?.holds[0]?.groups[0]?.boss.longName ||
							selectedStage?.holds[1]?.groups[0]?.boss.longName
						}
						group={
							selectedStage?.holds[0]?.groups[0]?.title ||
							selectedStage?.holds[1]?.groups[0]?.title
						}
						role='Главный согласующий'
					/>
					{page === 'stageForSuccess' && (
						<>
							<Divider className={styles.divider} />
							<Buttons
								selectedStage={selectedStage}
								isLoading={isLoading}
								isSuccess={isSuccess}
							/>
						</>
					)}
					<Divider className={styles.divider} />
					<ListTasks
						group={selectedStage?.holds[0]?.groups[0]?.title}
						page={page}
					/>
				</>
			)}
		</Box>
	)
}

export default SelectedStage
