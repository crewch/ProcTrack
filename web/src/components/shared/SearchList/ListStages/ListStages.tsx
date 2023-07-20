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
import { changeOpenedStage } from '../../../../store/processStageSlice/processStageSlice'
import { FC, memo, useMemo } from 'react'
import { useQuery } from '@tanstack/react-query'
import { Stage } from '../../../../shared/interfaces/stage'
import { stageService } from '../../../../services/stage'
import { useGetUserData } from '../../../../hooks/userDataHook'
import ListImg from '../../ListImg/ListImg'
import styles from './StagesListStyle.module.scss'

interface StagesListProps {
	textForSearchProcess: string
}

const StagesList: FC<StagesListProps> = memo(({ textForSearchProcess }) => {
	const openedStage = useAppSelector(state => state.processStage.openedStage)
	const dispatch = useAppDispatch()

	const userData = useGetUserData()

	const {
		data: listStages,
		isLoading,
		isSuccess,
	} = useQuery({
		queryKey: ['stagesAllByUserId'],
		queryFn: () => stageService.getStageAllByUserId(userData.id),
	})

	const filteredStages: Stage[] | null = useMemo(() => {
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
		<Box className={`${styles.container} h-full justify-between p-0`}>
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
