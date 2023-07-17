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
import { FC, memo } from 'react'
import { IProcess } from '../../../../interfaces/IApi/IApi'
import styles from '/src/styles/MainPageStyles/ContainerListProcessStyles/ListProcessStyles/ListProcess.module.scss'
import { IListProcessProps } from '../../../../interfaces/IMainPage/IContainerListProcess/IListProcess/IListProcess'
import ListImg from './ListImg/ListImg'
import { useFilterProcess } from '../../../../hooks/filterProcessHook'

const ListProcess: FC<IListProcessProps> = memo(({ textForSearchProcess }) => {
	const dispatch = useAppDispatch()
	const openedProcess = useAppSelector(state => state.processes.openedProcess)
	const settingsForSearch = useAppSelector(
		state => state.settings.settingsForSearch
	)

	const {
		data: allProcess,
		isLoading,
		isSuccess,
	} = useQuery({
		queryKey: ['allProcess'],
		queryFn: getProcessApi.getProcessAll,
	})

	const filteredProcesses: IProcess[] = useFilterProcess(
		isSuccess,
		allProcess,
		textForSearchProcess,
		settingsForSearch
	)

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
