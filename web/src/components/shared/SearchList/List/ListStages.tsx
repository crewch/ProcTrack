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
import { useAppSelector, useAppDispatch } from '@/hooks/reduxHooks'
import { stageService } from '@/services/stage'
import { changeOpenedStage } from '@/store/processStageSlice/processStageSlice'
import PaginationList from '../../PaginationList/PaginationList'
import styles from './List.module.scss'
import { getUserData } from '@/utils/getUserData'

const ListStages = () => {
	const openedStage = useAppSelector(state => state.processStage.openedStage)
	const dispatch = useAppDispatch()
	const filters = useAppSelector(state => state.filterStages)

	const userId = getUserData().id

	const [selectedPage, setSelectedPage] = useState(1)
	const limit = 14

	const queryClient = useQueryClient()

	const {
		data: allStages,
		isLoading: isLoadingAllStages,
		isSuccess: isSuccessAllStages,
	} = useQuery({
		queryKey: ['stagesAllByUserId', filters, selectedPage],
		queryFn: () =>
			stageService.getStageAllByUserId(userId, filters, limit, selectedPage),
		onSuccess: () => {
			queryClient.invalidateQueries({ queryKey: ['countStage'] })
		},
	})

	const { data: countStage, isSuccess: isSuccessCountStage } = useQuery({
		queryKey: ['countStage', filters],
		queryFn: () => stageService.getCountStage(userId, filters),
	})

	return (
		<>
			<List className={styles.list}>
				{isLoadingAllStages && <LinearProgress />}
				{isSuccessAllStages && allStages && !allStages.length && (
					<Typography variant='h4' className={styles.typography}>
						Этапов нет
					</Typography>
				)}
				{isSuccessAllStages &&
					allStages &&
					allStages.map((stage, index) => (
						<ListItem
							disablePadding
							key={index}
							className={classNames({
								[styles.openedWrap]: openedStage === stage.id,
							})}
						>
							<ListImg status={stage.status} />
							<ListItemButton
								className={styles.opened}
								onClick={() => dispatch(changeOpenedStage({ id: stage.id }))}
							>
								<ListItemText>
									<Typography
										className={classNames({
											[styles.openedText]: openedStage === stage.id,
										})}
									>
										{stage.processName} - {stage.title}
									</Typography>
								</ListItemText>
							</ListItemButton>
						</ListItem>
					))}
			</List>
			{isSuccessCountStage && countStage ? (
				<PaginationList
					count={Math.ceil(countStage / limit)}
					selectedPage={selectedPage}
					setSelectedPage={setSelectedPage}
				/>
			) : (
				<></>
			)}
		</>
	)
}

export default ListStages
