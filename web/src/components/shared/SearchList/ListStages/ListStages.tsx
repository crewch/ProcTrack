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
import { useQuery } from '@tanstack/react-query'
import { stageService } from '../../../../services/stage'
import { useGetUserData } from '../../../../hooks/userDataHook'
import ListImg from '../../../ui/ListImg/ListImg'
import styles from './ListStages.module.scss'

const ListStages = () => {
	const openedStage = useAppSelector(state => state.processStage.openedStage)
	const dispatch = useAppDispatch()
	const filters = useAppSelector(state => state.filterStages)

	const userData = useGetUserData()

	const {
		data: listStages,
		isLoading,
		isSuccess,
	} = useQuery({
		queryKey: ['stagesAllByUserId', filters],
		queryFn: () => stageService.getStageAllByUserId(userData.id, filters),
	})

	return (
		<List className={styles.list}>
			{isLoading && <LinearProgress />}
			{isSuccess && listStages && !listStages.length && (
				<Typography variant='h4' className={styles.typography}>
					Этапов нет
				</Typography>
			)}
			{isSuccess &&
				listStages &&
				listStages.map((stage, index) => (
					<ListItem
						disablePadding
						key={index}
						className={
							openedStage === stage.id
								? styles.openedProcessWrap
								: styles.closedProcessWrap
						}
					>
						<ListImg status={stage.status} />
						<ListItemButton
							className={styles.openedProcess}
							onClick={() => dispatch(changeOpenedStage({ id: stage.id }))}
						>
							<ListItemText>
								<Typography
									className={
										openedStage === stage.id
											? styles.openedProcessText
											: styles.closedProcessText
									}
								>
									{stage.title}
								</Typography>
							</ListItemText>
						</ListItemButton>
					</ListItem>
				))}
		</List>
	)
}

export default ListStages
