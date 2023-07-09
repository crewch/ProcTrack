import { Box, Divider, LinearProgress } from '@mui/material'
import UserField from '../SelectedProcess/InfoProcess/UserField/UserField'
import DateInfo from '../SelectedProcess/InfoProcess/DateInfoField/DateInfo'
import HeaderField from './HeaderField/HeaderField'
import ListTasks from './ListTasks/ListTasks'
import { useQuery } from '@tanstack/react-query'
import { useAppSelector } from '../../../hooks/reduxHooks'
import { getStageApi } from '../../../api/getStageApi'
import styles from '/src/styles/MainPageStyles/SelectedStageStyles/SelectedStage.module.scss'

const SelectedStage = () => {
	const openedStageID = useAppSelector(state => state.processes.openedStage)

	const {
		data: selectedStage,
		isLoading,
		isSuccess,
	} = useQuery({
		queryKey: ['stageId', openedStageID],
		queryFn: () => getStageApi.getStageId(openedStageID),
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
						}
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
						page='main'
					/>
					<Divider className={styles.divider} />
					<UserField
						responsible={
							// selectedStage.holds[0].users.length
							// 	? selectedStage.holds[0].users[0].longName
							// 	: selectedStage.holds.length > 1
							// 	? selectedStage.holds[1].users[0].longName
							// 	: 'Вся группа'
							selectedStage?.holds[0]?.groups[0]?.boss.longName ||
							selectedStage?.holds[1]?.groups[0]?.boss.longName
						}
						group={
							selectedStage?.holds[0]?.groups[0]?.title ||
							selectedStage?.holds[1]?.groups[0]?.title
						}
						role='Главный согласующий'
					/>
					<Divider className={styles.divider} />
					<ListTasks group={selectedStage.holds[0].groups[0].title} />
				</>
			)}
		</Box>
	)
}

export default SelectedStage
