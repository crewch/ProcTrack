import {
	Box,
	Button,
	List,
	ListItem,
	ListItemButton,
	ListItemText,
	Typography,
} from '@mui/material'
import { useAppDispatch, useAppSelector } from '../../../../hooks/reduxHooks'
import { changeOpenedStage } from '../../../../store/processSlice/processSlice'
import styles from '/src/styles/MainPageStyles/SelectedProcessStyles/StagesListStyles/StagesListStyle.module.scss'

const StagesList = () => {
	const openedStage = useAppSelector(state => state.processes.openedStage)
	const dispatch = useAppDispatch()

	const stagesList = [
		{ name: '1 этап', status: 'inprogress' },
		{ name: '2 этап', status: 'completed' },
		{ name: '3 этап', status: 'rejected' },
		{ name: '4 этап', status: 'inprogress' },
		{ name: '5 этап', status: 'completed' },
		{ name: '6 этап', status: 'completed' },
		{ name: '7 этап', status: 'inprogress' },
		{ name: '8 этап', status: 'inprogress' },
		{ name: '9 этап', status: 'inprogress' },
		{ name: '10 этап', status: 'completed' },
		{ name: '11 этап', status: 'rejected' },
		{ name: '12 этап', status: 'inprogress' },
		{ name: '13 этап', status: 'inprogress' },
		{ name: '14 этап', status: 'completed' },
		{ name: '15 этап', status: 'completed' },
	]

	return (
		<Box className={styles.container}>
			<List className={styles.list}>
				{stagesList.map((stage, index) => (
					<ListItem
						disablePadding
						key={index}
						sx={{ pl: 0, pr: 1.5, py: 0.3 }}
						className={(() =>
							openedStage === stage.name ? styles.openedProcessWrap : '')()}
					>
						<img
							src={`src/assets/${stage.status}.svg`}
							style={{ marginRight: '14px', marginLeft: '6px' }}
						/>
						<ListItemButton
							className={styles.openedProcess}
							onClick={() => dispatch(changeOpenedStage({ name: stage.name }))}
						>
							<ListItemText>
								<Typography
									sx={{ color: '#333333' }}
									className={(() =>
										openedStage === stage.name
											? styles.openedProcessText
											: 'color: "#333333"')()}
								>
									{stage.name}
								</Typography>
							</ListItemText>
						</ListItemButton>
					</ListItem>
				))}
			</List>
			<Button
				className={styles.btn}
				variant='contained'
				endIcon={<img src='/src/assets/table.svg' />}
			>
				табличное представление
			</Button>
		</Box>
	)
}

export default StagesList
