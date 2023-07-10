import {
	Box,
	LinearProgress,
	List,
	ListItem,
	ListItemButton,
	ListItemText,
	Typography,
} from '@mui/material'
import { useAppDispatch, useAppSelector } from '../../../../../hooks/reduxHooks'
import { changeOpenedStage } from '../../../../../store/processSlice/processSlice'
import styles from '/src/styles/MainPageStyles/SelectedProcessStyles/StagesListStyles/StagesListStyle.module.scss'
import { IStage } from '../../../../../interfaces/IApi/IGetStageApi'
import { FC, useMemo } from 'react'
import { useQuery } from '@tanstack/react-query'
import { getStageApi } from '../../../../../api/getStageApi'
import { IUser } from '../../../../../interfaces/IApi/IApi'

const StagesList: FC<{ textForSearchProcess: string }> = ({
	textForSearchProcess,
}) => {
	const openedStage = useAppSelector(state => state.processes.openedStage)
	const dispatch = useAppDispatch()

	const userDataText = localStorage.getItem('UserData')
	const userData: IUser = userDataText && JSON.parse(userDataText)

	const {
		data: stagesList,
		isLoading,
		isSuccess,
	} = useQuery({
		queryKey: ['stagesStageForSuccess'],
		queryFn: () => getStageApi.getStageALLStageForSuccess(userData.id),
	})

	const filteredProcesses: IStage[] | null = useMemo(() => {
		if (stagesList) {
			return stagesList.filter(process =>
				process.title
					.toLocaleLowerCase()
					.includes(textForSearchProcess?.toLocaleLowerCase())
			)
		}

		return null
	}, [stagesList, textForSearchProcess])

	return (
		<Box className={`${styles.container} ${styles.stageForSuccessContainer}`}>
			{isLoading && <LinearProgress />}
			{isSuccess && stagesList && filteredProcesses && (
				<>
					<List className={styles.list}>
						{filteredProcesses
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
				</>
			)}
		</Box>
	)
}

export default StagesList
