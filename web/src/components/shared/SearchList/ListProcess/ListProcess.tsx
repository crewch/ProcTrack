import {
	LinearProgress,
	List,
	ListItem,
	ListItemButton,
	ListItemText,
	Typography,
} from '@mui/material'
import { useAppDispatch, useAppSelector } from '../../../../hooks/reduxHooks'
import { changeOpenedProcess } from '../../../../store/processStageSlice/processStageSlice'
import { useQuery } from '@tanstack/react-query'
import { processService } from '../../../../services/process'
import { useGetUserData } from '../../../../hooks/userDataHook'
import styles from './ListProcess.module.scss'
import ListImg from '../../ListImg/ListImg'

const ListProcess = () => {
	const dispatch = useAppDispatch()
	const openedProcess = useAppSelector(
		state => state.processStage.openedProcess
	)
	const settings = useAppSelector(state => state.settingProcess)

	const userId = useGetUserData().id

	const {
		data: allProcess,
		isLoading,
		isSuccess,
	} = useQuery({
		queryKey: ['allProcess', settings],
		queryFn: () => processService.getProcessAll(userId, settings),
	})

	return (
		<List className={styles.list}>
			{isLoading && <LinearProgress />}
			{isSuccess && allProcess && !allProcess.length && (
				<Typography variant='h4' className={styles.typography}>
					Процессов нет
				</Typography>
			)}
			{isSuccess &&
				allProcess &&
				allProcess.map((process, index) => (
					<ListItem
						disablePadding
						key={index}
						className={
							openedProcess === process.id ? styles.openedProcessWrap : ''
						}
					>
						<ListImg status={process.status} />
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
