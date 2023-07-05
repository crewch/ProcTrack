import {
	Accordion,
	AccordionDetails,
	AccordionSummary,
	List,
	Typography,
} from '@mui/material'
import { IListTasks } from '../../../../interfaces/IMainPage/ISelectedStage/IListTasks/IListTasks'
import ExpandMoreIcon from '@mui/icons-material/ExpandMore'
import styles from '/src/styles/MainPageStyles/SelectedStageStyles/ListTasksStyles/ListTasksStyles.module.scss'
import ListTask from './ListTask/ListTask'

const ListTasks = () => {
	const listTasks: IListTasks[] = [
		// {
		// 	numberOfTask: 'Первое задание',
		// 	startDate: 'пт, 22 декабря 2023, 16:30',
		// 	endDate: 'ср, 27 декабря 2023, 12:00',
		// 	successDate: 'ср, 27 декабря 2023, 12:11',
		// 	status: 'согласовано',
		// 	roleAuthor: 'Согласующий',
		// 	author: 'Петр Петров',
		// 	group: 'группа согласующего',
		// 	remarks: [
		// 		{
		// 			author: 'Сергей Сергеев',
		// 			date: 'ср, 27 декабря 2023, 13:32',
		// 			text: 'Всё в порядке - утверждаю',
		// 		},
		// 		{
		// 			author: 'Петр Петров',
		// 			date: 'ср, 27 декабря 2023, 13:32',
		// 			text: 'Исправил выявленные проблемы',
		// 		},
		// 		{
		// 			author: 'Сергей Сергеев',
		// 			date: 'ср, 27 декабря 2023, 13:32',
		// 			text: 'Необходимо исправить недостатки',
		// 		},
		// 	],
		// },
		// {
		// 	numberOfTask: 'Второе задание',
		// 	startDate: 'пт, 22 декабря 2023, 16:30',
		// 	endDate: 'ср, 27 декабря 2023, 12:00',
		// 	successDate: 'ср, 27 декабря 2023, 12:11',
		// 	status: 'в процессе',
		// 	roleAuthor: 'Согласующий',
		// 	author: 'Сергей Сергеев',
		// 	group: 'группа согласующего',
		// 	remarks: [
		// 		{
		// 			author: 'Сергей Сергеев',
		// 			date: 'ср, 27 декабря 2023, 13:32',
		// 			text: 'Всё в порядке - утверждаю',
		// 		},
		// 		{
		// 			author: 'Петр Петров',
		// 			date: 'ср, 27 декабря 2023, 13:32',
		// 			text: 'Исправил выявленные проблемы',
		// 		},
		// 		{
		// 			author: 'Сергей Сергеев',
		// 			date: 'ср, 27 декабря 2023, 13:32',
		// 			text: 'Необходимо исправить недостатки',
		// 		},
		// 	],
		// },
		// {
		// 	numberOfTask: 'Третье задание',
		// 	startDate: 'пт, 22 декабря 2023, 16:30',
		// 	endDate: 'ср, 27 декабря 2023, 12:00',
		// 	successDate: 'ср, 27 декабря 2023, 12:11',
		// 	status: 'отклонено',
		// 	roleAuthor: 'Согласующий',
		// 	author: 'Иван Петров',
		// 	group: 'группа согласующего',
		// 	remarks: [
		// 		{
		// 			author: 'Сергей Сергеев',
		// 			date: 'ср, 27 декабря 2023, 13:32',
		// 			text: 'Всё в порядке - утверждаю',
		// 		},
		// 		{
		// 			author: 'Петр Петров',
		// 			date: 'ср, 27 декабря 2023, 13:32',
		// 			text: 'Исправил выявленные проблемы',
		// 		},
		// 		{
		// 			author: 'Сергей Сергеев',
		// 			date: 'ср, 27 декабря 2023, 13:32',
		// 			text: 'Необходимо исправить недостатки',
		// 		},
		// 	],
		// },
	]

	return (
		<List component='nav' className={styles.list}>
			{!listTasks.length && (
				<Typography variant='h4' className={styles.typography}>
					Процессов нет
				</Typography>
			)}
			{listTasks.map((task, index) => (
				<Accordion disableGutters key={index} className={styles.accordion}>
					<AccordionSummary expandIcon={<ExpandMoreIcon />}>
						{task.status === 'согласовано' && (
							<img src='/completed.svg' className={styles.img} />
						)}
						{task.status === 'в процессе' && (
							<img src='/inprogress.svg' className={styles.img} />
						)}
						{task.status === 'отклонено' && (
							<img src='/rejected.svg' className={styles.img} />
						)}
						<Typography className={styles.title}>
							{task.numberOfTask}
						</Typography>
					</AccordionSummary>
					<AccordionDetails>
						<ListTask
							startDate={task.startDate}
							endDate={task.endDate}
							successDate={task.successDate}
							roleAuthor={task.roleAuthor}
							author={task.author}
							group={task.group}
							remarks={task.remarks}
						/>
					</AccordionDetails>
				</Accordion>
			))}
		</List>
	)
}

export default ListTasks
