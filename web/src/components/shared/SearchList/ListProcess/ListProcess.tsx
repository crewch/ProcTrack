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
import { useQuery, useQueryClient } from '@tanstack/react-query'
import { processService } from '../../../../services/process'
import { useGetUserData } from '../../../../hooks/userDataHook'
import ListImg from '../../../ui/ListImg/ListImg'
import styles from './ListProcess.module.scss'
import { useState } from 'react'
import PaginationList from '../../PaginationList/PaginationList'

const ListProcess = () => {
	const dispatch = useAppDispatch()
	const openedProcess = useAppSelector(
		state => state.processStage.openedProcess
	)
	const filters = useAppSelector(state => state.filterProcess)

	const userId = useGetUserData().id

	const [selectedPage, setSelectedPage] = useState(1)
	const limit = 14

	const queryClient = useQueryClient()

	const {
		data: allProcess,
		isLoading: isLoadingAllProcess,
		isSuccess: isSuccessAllProcess,
	} = useQuery({
		queryKey: ['allProcess', filters, selectedPage],
		queryFn: () =>
			processService.getProcessAll(userId, filters, limit, selectedPage),
		onSuccess: () => {
			queryClient.invalidateQueries({ queryKey: ['countProcess'] })
		},
	})

	const { data: countProcess, isSuccess: isSuccessCountProcess } = useQuery({
		queryKey: ['countProcess', filters],
		queryFn: () => processService.getCountProcess(userId, filters),
	})

	return (
		<>
			<List className={styles.list}>
				{isLoadingAllProcess && <LinearProgress />}
				{isSuccessAllProcess && allProcess && !allProcess.length && (
					<Typography variant='h4' className={styles.typography}>
						Процессов нет
					</Typography>
				)}
				{isSuccessAllProcess &&
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
								onClick={() =>
									dispatch(changeOpenedProcess({ id: process.id }))
								}
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
			{isSuccessCountProcess && countProcess && (
				<PaginationList
					count={Math.ceil(countProcess / limit)}
					selectedPage={selectedPage}
					setSelectedPage={setSelectedPage}
				/>
			)}
		</>
	)
}

export default ListProcess
