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
import styles from '/src/styles/MainPageStyles/SelectedProcessStyles/StagesListStyles/StagesListStyle.module.scss'
import DataDialog from '../../ContainerListProcess/DataDialog/DataDialog'
import DataGraph from '../../Dialogs/DataGraph/DataGraph'
import { useQuery } from '@tanstack/react-query'
import { getStageApi } from '../../../../api/getStageApi'

const StagesList = () => {
	const openedProcess = useAppSelector(state => state.processes.openedProcess)
	const openedStage = useAppSelector(state => state.processes.openedStage)
	const dispatch = useAppDispatch()

	const {
		data: stagesList,
		isLoading,
		isSuccess,
	} = useQuery({
		queryKey: ['stages', openedProcess],
		queryFn: () => getStageApi.getStageALL(openedProcess),
	})

	return (
		<Box className={styles.container}>
			{isLoading && <LinearProgress />}
			{isSuccess && stagesList && (
				<>
					<List className={styles.list}>
						{stagesList.map((stage, index) => (
							<ListItem
								disablePadding
								key={index}
								className={
									openedStage === stage.id
										? styles.openedProcessWrap
										: styles.closedProcessWrap
								}
							>
								{stage.status && (
									<img
										src='/rejected.svg'
										loading='lazy'
										className={styles.img}
									/>
								)}

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
					<DataDialog title='Графовое представление' icon='graph'>
						<DataGraph />
					</DataDialog>
				</>
			)}
		</Box>
	)
}

export default StagesList
