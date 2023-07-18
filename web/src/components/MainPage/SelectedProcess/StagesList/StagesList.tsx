import {
	Box,
	LinearProgress,
	List,
	ListItem,
	ListItemButton,
	ListItemText,
	Typography,
} from '@mui/material'
import { useAppDispatch, useAppSelector } from '../../../../hooks/reduxHooks'
import { changeOpenedStage } from '../../../../store/processSlice/processSlice'
import DataDialog from '../../ContainerListProcess/DataDialog/DataDialog'
import DataGraph from '../../Dialogs/DataGraph/DataGraph'
import { useQuery } from '@tanstack/react-query'
import { getStageApi } from '../../../../api/getStageApi'
import ListImg from './ListImg/ListImg'
import { FC } from 'react'
import { ISelectedStageProps } from '../../../../interfaces/IMainPage/ISelectedStage/ISelectedStage'
import styles from '/src/styles/MainPageStyles/SelectedProcessStyles/StagesListStyles/StagesListStyle.module.scss'

const StagesList: FC<ISelectedStageProps> = ({ page }) => {
	const openedProcess = useAppSelector(state => state.processes.openedProcess)
	const openedStage = useAppSelector(state => state.processes.openedStage)
	const dispatch = useAppDispatch()

	const {
		data: stagesList,
		isLoading,
		isSuccess,
	} = useQuery({
		queryKey: ['stages', openedProcess],
		queryFn: () => getStageApi.getStageAll(openedProcess),
	})

	return (
		<Box
			className={`${styles.container} ${
				page === 'stageForSuccess' ? styles.stageForSuccessSmallContainer : ''
			}`}
		>
			{isLoading && <LinearProgress />}
			{isSuccess && stagesList && (
				<>
					<List className={styles.list}>
						{stagesList
							.sort((a, b) => a.id - b.id)
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
					<DataDialog title='Графовое представление' icon='graph'>
						<DataGraph />
					</DataDialog>
				</>
			)}
		</Box>
	)
}

export default StagesList
