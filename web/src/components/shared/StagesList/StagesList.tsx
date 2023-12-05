import {
	Box,
	List,
	ListItem,
	ListItemButton,
	ListItemText,
	Skeleton,
	Typography,
} from '@mui/material'
import { useQuery } from '@tanstack/react-query'
import ListImg from '@/components/ui/ListImg/ListImg'
import { useAppSelector, useAppDispatch } from '@/hooks/reduxHooks'
import { stageService } from '@/services/stage'
import { changeOpenedStage } from '@/store/processStageSlice/processStageSlice'
import DataGraph from '../DataGraph/DataGraph'
import FullScreenDialogButton from '../FullScreenDialogButton/FullScreenDialogButton'
import styles from './StagesList.module.scss'

const StagesList = () => {
	const openedProcess = useAppSelector(
		state => state.processStage.openedProcess
	)
	const openedStage = useAppSelector(state => state.processStage.openedStage)
	const dispatch = useAppDispatch()

	const {
		data: stagesList,
		isLoading,
		isSuccess,
	} = useQuery({
		queryKey: ['stages', openedProcess],
		queryFn: () => stageService.getStageAllByProcessId(openedProcess),
	})

	return (
		<Box className={styles.container}>
			{isLoading && <Skeleton variant='rounded' height='100%' />}
			{isSuccess && stagesList && (
				<>
					<List className={styles.list}>
						{stagesList
							// .sort((a, b) => a.id - b.id)
							.map((stage, index) => (
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
										onClick={() =>
											dispatch(changeOpenedStage({ id: stage.id }))
										}
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
					<FullScreenDialogButton
						title='Графовое представление'
						icon='graph'
						fullWidth
					>
						<DataGraph />
					</FullScreenDialogButton>
				</>
			)}
		</Box>
	)
}

export default StagesList
