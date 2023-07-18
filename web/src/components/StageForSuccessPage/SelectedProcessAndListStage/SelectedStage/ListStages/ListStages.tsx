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
import { IStage } from '../../../../../interfaces/IApi/IGetStageApi'
import { FC, memo, useMemo } from 'react'
import { useQuery } from '@tanstack/react-query'
import { getStageApi } from '../../../../../api/getStageApi'
import { IUser } from '../../../../../interfaces/IApi/IApi'
import ListImg from '../../../../MainPage/SelectedProcess/StagesList/ListImg/ListImg'
import { IListProcessProps } from '../../../../../interfaces/IMainPage/IContainerListProcess/IListProcess/IListProcess'
import styles from '/src/styles/MainPageStyles/SelectedProcessStyles/StagesListStyles/StagesListStyle.module.scss'

const StagesList: FC<IListProcessProps> = memo(({ textForSearchProcess }) => {
	const openedStage = useAppSelector(state => state.processes.openedStage)
	const dispatch = useAppDispatch()

	const userDataText = localStorage.getItem('UserData')
	const userData: IUser = userDataText && JSON.parse(userDataText)

	const {
		data: listStages,
		isLoading,
		isSuccess,
	} = useQuery({
		queryKey: ['stagesStageForSuccess'],
		queryFn: () => getStageApi.getStageAllStageForSuccess(userData.id),
	})

	const filteredStages: IStage[] | null = useMemo(() => {
		if (listStages) {
			return listStages
				.filter(process =>
					process.title
						.toLocaleLowerCase()
						.includes(textForSearchProcess?.toLocaleLowerCase())
				)
				.sort((a, b) => b.id - a.id)
		}

		return null
	}, [listStages, textForSearchProcess])

	return (
		<Box className={`${styles.container} ${styles.stageForSuccessContainer}`}>
			{isLoading && <LinearProgress />}
			{isSuccess && listStages && filteredStages && (
				<>
					<List component='nav' className={styles.list}>
						{filteredStages.map((stage, index) => (
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
				</>
			)}
		</Box>
	)
})

export default StagesList
