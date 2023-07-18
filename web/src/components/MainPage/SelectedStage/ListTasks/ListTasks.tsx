import {
	Accordion,
	AccordionDetails,
	AccordionSummary,
	LinearProgress,
	List,
	Typography,
} from '@mui/material'
import ExpandMoreIcon from '@mui/icons-material/ExpandMore'
import ListTask from './ListTask/ListTask'
import { useAppSelector } from '../../../../hooks/reduxHooks'
import { useQuery } from '@tanstack/react-query'
import { getTaskApi } from '../../../../api/getTaskApi'
import { FC, memo } from 'react'
import { IListTasksProps } from '../../../../interfaces/IMainPage/ISelectedStage/IListTasks/IListTasks'
import styles from '/src/styles/MainPageStyles/SelectedStageStyles/ListTasksStyles/ListTasksStyles.module.scss'

const ListTasks: FC<IListTasksProps> = memo(({ group, page }) => {
	const selectedStage = useAppSelector(state => state.processes.openedStage)

	const {
		data: tasks,
		isLoading,
		isSuccess,
	} = useQuery({
		queryKey: ['tasks', selectedStage],
		queryFn: () => getTaskApi.getTaskAll(selectedStage),
	})

	return (
		<List component='nav' className={styles.list}>
			{isLoading && <LinearProgress />}
			{isSuccess &&
				tasks &&
				tasks.map((task, index) => (
					<Accordion disableGutters key={index} className={styles.accordion}>
						<AccordionSummary expandIcon={<ExpandMoreIcon />}>
							<Typography className={styles.title}>{task.title}</Typography>
						</AccordionSummary>
						<AccordionDetails>
							<ListTask
								startDate={task.startedAt}
								endDate={task.endVerificationDate}
								successDate={task.approvedAt}
								roleAuthor={'Согласующий'}
								author={task.user.longName || 'Согласующий ещё не принял'}
								group={group}
								remarks={task.comments}
								taskId={task.id}
								page={page}
							/>
						</AccordionDetails>
					</Accordion>
				))}
		</List>
	)
})

export default ListTasks
