import { Box, Divider, Skeleton } from '@mui/material'
import ListTasks from './ListTasks/ListTasks'
import { useQuery } from '@tanstack/react-query'
import { FC } from 'react'
import Buttons from './Buttons/Buttons'
import { useAppSelector } from '@/hooks/reduxHooks'
import { stageService } from '@/services/stage'
import DateInfo from '../DateInfo/DateInfo'
import InfoField from '../InfoField/InfoField'
import UserInfo from '../UserInfo/UserInfo'
import styles from './SelectedStage.module.scss'

interface SelectedStageProps {
	page: 'release' | 'approval'
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
			{isLoading && <Skeleton variant='rounded' height='100%' />}
			{isSuccess && selectedStage && (
				<>
					<InfoField
						name={selectedStage.title}
						status={selectedStage.status}
						nameOfGroup={
							selectedStage?.holds[0]?.groups[0]?.title ||
							selectedStage?.holds[1]?.groups[0]?.title
						} //BUG: тут проблема при статусе ещё не начат
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
					<UserInfo
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
					{page === 'approval' && (
						<Buttons
							selectedStage={selectedStage}
							isLoading={isLoading}
							isSuccess={isSuccess}
						/>
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
