import {
	Box,
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

interface IStagesList {
	name: string
	status: string
}

const StagesList = () => {
	const openedStage = useAppSelector(state => state.processes.openedStage)
	const dispatch = useAppDispatch()

	const stagesList: IStagesList[] = [
		// { name: '1 этап', status: 'inprogress' },
		// { name: '2 этап', status: 'completed' },
		// { name: '3 этап', status: 'rejected' },
		// { name: '4 этап', status: 'inprogress' },
		// { name: '5 этап', status: 'completed' },
		// { name: '6 этап', status: 'completed' },
		// { name: '7 этап', status: 'inprogress' },
		// { name: '8 этап', status: 'inprogress' },
		// { name: '9 этап', status: 'inprogress' },
		// { name: '10 этап', status: 'completed' },
		// { name: '11 этап', status: 'rejected' },
		// { name: '12 этап', status: 'inprogress' },
		// { name: '13 этап', status: 'inprogress' },
		// { name: '14 этап', status: 'completed' },
		// { name: '15 этап', status: 'completed' },
	]

	return (
		<Box className={styles.container}>
			<List className={styles.list}>
				{stagesList.map((stage, index) => (
					<ListItem
						disablePadding
						key={index}
						className={
							openedStage === stage.name
								? styles.openedProcessWrap
								: styles.closedProcessWrap
						}
					>
						<img
							src={`/${stage.status}.svg`}
							className={styles.img}
							height='20px'
							width='20px'
						/>
						<ListItemButton
							className={styles.openedProcess}
							onClick={() => dispatch(changeOpenedStage({ name: stage.name }))}
						>
							<ListItemText>
								<Typography
									className={
										openedStage === stage.name
											? styles.openedProcessText
											: styles.closedProcessText
									}
								>
									{stage.name}
								</Typography>
							</ListItemText>
						</ListItemButton>
					</ListItem>
				))}
			</List>
			<DataDialog title='Графовое представление' icon='graph'>
				<DataGraph />
			</DataDialog>
		</Box>
	)
}

export default StagesList
