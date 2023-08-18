import {
	LinearProgress,
	List,
	ListItem,
	ListItemButton,
	ListItemText,
	Typography,
} from '@mui/material'
import { useQuery, useQueryClient } from '@tanstack/react-query'
import { useState } from 'react'
import classNames from 'classnames'
import ListImg from '@/components/ui/ListImg/ListImg'
import { useAppDispatch, useAppSelector } from '@/hooks/reduxHooks'
import { processService } from '@/services/process'
import { changeOpenedProcess } from '@/store/processStageSlice/processStageSlice'
import PaginationList from '../../PaginationList/PaginationList'
import styles from './List.module.scss'

const ListProcess = () => {
	const dispatch = useAppDispatch()
	const openedProcess = useAppSelector(
		state => state.processStage.openedProcess
	)
	const filters = useAppSelector(state => state.filterProcess)

	const [selectedPage, setSelectedPage] = useState(1)
	const limit = 14

	const queryClient = useQueryClient()

	const {
		data: allProcess,
		isLoading: isLoadingAllProcess,
		isSuccess: isSuccessAllProcess,
	} = useQuery({
		queryKey: ['allProcess', filters, selectedPage],
		queryFn: () => processService.getProcessAll(filters, limit, selectedPage),
		onSuccess: () => {
			queryClient.invalidateQueries({ queryKey: ['countProcess'] })
		},
	})

	const { data: countProcess, isSuccess: isSuccessCountProcess } = useQuery({
		queryKey: ['countProcess', filters],
		queryFn: () => processService.getCountProcess(filters),
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
							className={classNames({
								[styles.openedWrap]: openedProcess === process.id,
							})}
						>
							<ListImg status={process.status} />
							<ListItemButton
								className={styles.opened}
								onClick={() =>
									dispatch(changeOpenedProcess({ id: process.id }))
								}
							>
								<ListItemText>
									<Typography
										className={classNames({
											[styles.openedText]: openedProcess === process.id,
										})}
									>
										{process.title}
									</Typography>
								</ListItemText>
							</ListItemButton>
						</ListItem>
					))}
			</List>
			{isSuccessCountProcess && countProcess ? (
				<PaginationList
					count={Math.ceil(countProcess / limit)}
					selectedPage={selectedPage}
					setSelectedPage={setSelectedPage}
				/>
			) : (
				<></>
			)}
		</>
	)
}

export default ListProcess
