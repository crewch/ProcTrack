import {
	LinearProgress,
	List,
	ListItem,
	ListItemButton,
	ListItemText,
	Typography,
} from '@mui/material'
import { useAppDispatch, useAppSelector } from '../../../../hooks/reduxHooks'
import { changeOpenedStage } from '../../../../store/processStageSlice/processStageSlice'
import { useQuery, useQueryClient } from '@tanstack/react-query'
import { stageService } from '../../../../services/stage'
import { useGetUserData } from '../../../../hooks/userDataHook'
import ListImg from '../../../ui/ListImg/ListImg'
import { useState } from 'react'
import PaginationList from '../../PaginationList/PaginationList'
import styles from './List.module.scss'
import classNames from 'classnames'

const ListStages = () => {
	const openedStage = useAppSelector(state => state.processStage.openedStage)
	const dispatch = useAppDispatch()
	const filters = useAppSelector(state => state.filterStages)

	const userId = useGetUserData().id

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
										{stage.title}
									</Typography>
								</ListItemText>
							</ListItemButton>
						</ListItem>
					))}
			</List>
			{isSuccessCountStage && countStage && (
				<PaginationList
					count={Math.ceil(countStage / limit)}
					selectedPage={selectedPage}
					setSelectedPage={setSelectedPage}
				/>
			)}
		</>
	)
}

export default ListStages
