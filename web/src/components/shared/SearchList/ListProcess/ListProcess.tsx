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
import { FC, memo, useMemo } from 'react'
import { Process } from '../../../../shared/interfaces/process'
import { processService } from '../../../../services/process'
import { useGetUserData } from '../../../../hooks/userDataHook'
import styles from './ListProcess.module.scss'
import ListImg from '../../ListImg/ListImg'

interface ListProcessProps {
	textForSearchProcess: string
}

const ListProcess: FC<ListProcessProps> = memo(({ textForSearchProcess }) => {
	const dispatch = useAppDispatch()
	const openedProcess = useAppSelector(
		state => state.processStage.openedProcess
	)
	const settingsForSearch = useAppSelector(
		state => state.searchSettings.settingsForSearch
	)

	const userId = useGetUserData().id

	const {
		data: allProcess,
		isLoading,
		isSuccess,
	} = useQuery({
		queryKey: ['allProcess'],
		queryFn: () => processService.getProcessAll(userId),
	})

	const filteredProcesses: Process[] = useMemo(() => {
		if (isSuccess && allProcess) {
			return allProcess
				.sort((a, b) => b.id - a.id)
				.filter(process =>
					process.title
						.toLocaleLowerCase()
						.includes(textForSearchProcess.toLocaleLowerCase())
				)
				.filter(item => {
					if (!settingsForSearch.length) return item

					if (
						settingsForSearch.includes(item.priority) ||
						settingsForSearch.includes(item.type) ||
						settingsForSearch.includes(item.status)
					) {
						return item
					}
				})
		}

		return []
	}, [allProcess, isSuccess, settingsForSearch, textForSearchProcess])

	return (
		<List className={styles.list}>
			{isLoading && <LinearProgress />}
			{isSuccess && filteredProcesses && !filteredProcesses.length && (
				<Typography variant='h4' className={styles.typography}>
					Процессов нет
				</Typography>
			)}
			{isSuccess &&
				filteredProcesses &&
				filteredProcesses.map((process, index) => (
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
})

export default ListProcess
