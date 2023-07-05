import {
	LinearProgress,
	List,
	ListItem,
	ListItemButton,
	ListItemText,
	Typography,
} from '@mui/material'
import { useAppDispatch, useAppSelector } from '../../../../hooks/reduxHooks'
import { changeOpenedProcess } from '../../../../store/processSlice/processSlice'
import { useQuery } from '@tanstack/react-query'
import { getProcessApi } from '../../../../api/getProcessApi'
import styles from '/src/styles/MainPageStyles/ContainerListProcessStyles/ListProcessStyles/ListProcess.module.scss'

const ListProcess = () => {
	const dispatch = useAppDispatch()
	const openedProcess = useAppSelector(state => state.processes.openedProcess)

	const { data, isLoading, isSuccess } = useQuery({
		queryKey: ['allProcess'],
		queryFn: getProcessApi.getProcessAll,
	})

	return (
		<List className={styles.list}>
			{isLoading && <LinearProgress />}
			{isSuccess && data && !data.length && (
				<Typography variant='h4' className={styles.typography}>
					Процессов нет
				</Typography>
			)}
			{isSuccess &&
				data &&
				data.map((process, index) => (
					<ListItem
						disablePadding
						key={index}
						className={
							openedProcess === process.id ? styles.openedProcessWrap : ''
						}
					>
						{process.status === 'в процессе' && (
							<img src='/inprogress.svg' className={styles.img} />
						)}
						{process.status === 'завершен' && (
							<img src='/completed.svg' className={styles.img} />
						)}
						{process.status === 'остановлен' && (
							<img src='/pause.svg' className={styles.img} />
						)}
						{process.status === 'отменен' && (
							<img src='/rejected.svg' className={styles.img} />
						)}
						<ListItemButton
							className={styles.openedProcess}
							onClick={() => dispatch(changeOpenedProcess({ id: process.id }))}
						>
							<ListItemText>
								<Typography
									className={
										openedProcess === process.id
											? styles.openedProcessText
											: styles.closedProcessText
									}
								>
									{process.title}
								</Typography>
							</ListItemText>
						</ListItemButton>
					</ListItem>
				))}
		</List>
	)
}

export default ListProcess
