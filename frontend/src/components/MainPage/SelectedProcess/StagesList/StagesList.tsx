import {
	Box,
	List,
	ListItem,
	ListItemAvatar,
	ListItemButton,
	ListItemText,
	Typography,
} from '@mui/material'
import styles from '/src/styles/MainPageStyles/SelectedProcess/StagesList/StagesListStyle.module.css'
import { useAppDispatch, useAppSelector } from '../../../../hooks/reduxHooks'
import { changeOpenedStage } from '../../../../store/processSlice/processSlice'

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
		<Box
			sx={{
				height: '50%',
				width: '100%',
				backgroundColor: 'white',
				borderRadius: '8px',
				p: 2,
			}}
		>
			<List
				sx={{
					mt: 1,
					height: '100%',
					overflow: 'auto',
				}}
			>
				{stagesList.map((stage, index) => (
					<ListItem
						sx={{ pl: 0 }}
						key={index}
						className={(() =>
							openedStage === stage.name ? styles.openedProcessWrap : '')()}
					>
						<ListItemAvatar sx={{ display: 'flex', justifyContent: 'center' }}>
							<img src={`src/assets/${stage.status}.svg`} />
						</ListItemAvatar>
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
		</Box>
	)
}

export default StagesList
