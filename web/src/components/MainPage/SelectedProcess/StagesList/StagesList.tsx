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
						{stagesList
							.sort((a, b) => a.statusValue - b.statusValue)
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
									{stage.status === 'Согласовано' && (
										<img
											src='/completed.svg'
											loading='lazy'
											className={styles.img}
										/>
									)}
									{stage.status === 'Не начат' && (
										<img
											src='/stoppedProcess.svg'
											loading='lazy'
											className={styles.img}
										/>
									)}
									{stage.status === 'Согласовано-Блокировано' && (
										<img
											src='/lock.svg'
											loading='lazy'
											className={styles.img}
										/>
									)}
									{stage.status === 'Принят на проверку' && (
										<img
											src='/arrow-circle-down.svg'
											loading='lazy'
											className={styles.img}
										/>
									)}
									{stage.status === 'Отправлен на проверку' && (
										<img
											src='/arrow-circle-right.svg'
											loading='lazy'
											className={styles.img}
										/>
									)}
									{stage.status === 'Отменен' && (
										<img
											src='/rejected.svg'
											loading='lazy'
											className={styles.img}
										/>
									)}
									{stage.status === 'Остановлен' && (
										<img
											src='/pause-circle.svg'
											loading='lazy'
											className={styles.img}
										/>
									)}

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
					<DataDialog title='Графовое представление' icon='graph' page='main'>
						<DataGraph />
					</DataDialog>
				</>
			)}
		</Box>
	)
}

export default StagesList
