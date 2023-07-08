import {
	Accordion,
	AccordionDetails,
	AccordionSummary,
	LinearProgress,
	List,
	Typography,
} from '@mui/material'
import ExpandMoreIcon from '@mui/icons-material/ExpandMore'
import styles from '/src/styles/MainPageStyles/SelectedStageStyles/ListTasksStyles/ListTasksStyles.module.scss'
import ListTask from './ListTask/ListTask'
import { useAppSelector } from '../../../../hooks/reduxHooks'
import { useQuery } from '@tanstack/react-query'
import { getTaskApi } from '../../../../api/getTaskApi'

const ListTasks = () => {
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
			{/* {isSuccess && tasks && !tasks.length && (
				<Typography variant='h4' className={styles.typography}>
					Процессов нет
				</Typography>
			)} */}
			{isSuccess &&
				tasks &&
				tasks.map((task, index) => (
					<Accordion disableGutters key={index} className={styles.accordion}>
						<AccordionSummary expandIcon={<ExpandMoreIcon />}>
							{/* {task.status === 'согласовано' && (
								<img src='/completed.svg' className={styles.img} />
							)} 
							//TODO:
							*/}
							<Typography className={styles.title}>{task.title}</Typography>
						</AccordionSummary>
						<AccordionDetails>
							<ListTask
								startDate={task.startedAt}
								endDate={task.endVerificationDate}
								successDate={task.approvedAt}
								roleAuthor={
									task.user.roles ? task.user.roles[0] : 'Согласующий'
								}
								author={'Петр Петров'} //TODO:
								group={'группа согласующего'} //TODO:
								remarks={task.comments}
								taskId={task.id}
							/>
						</AccordionDetails>
					</Accordion>
				))}
		</List>
	)
}

export default ListTasks
