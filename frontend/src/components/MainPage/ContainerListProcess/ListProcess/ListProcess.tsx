import {
	List,
	ListItem,
	ListItemButton,
	ListItemText,
	Typography,
} from '@mui/material'
import { useAppDispatch, useAppSelector } from '../../../../hooks/reduxHooks'
import { changeOpenedProcess } from '../../../../store/processSlice/processSlice'
import styles from '/src/styles/MainPageStyles/ContainerListProcess/ListProcess/ListProcess.module.css'
import { FC, useMemo } from 'react'
import { IListProcessProps } from '../../../../interfaces/IMainPage/IContainerListProcess/IListProcessProps'

const listProcess = [
	{ name: '1 процесс' },
	{ name: '2 процесс' },
	{ name: '3 процесс' },
	{ name: '4 процесс' },
	{ name: '5 процесс' },
	{ name: '6 процесс' },
	{ name: '7 процесс' },
	{ name: '8 процесс' },
	{ name: '9 процесс' },
	{ name: '10 процесс' },
	{ name: '11 процесс' },
	{ name: '12 процесс' },
	{ name: '13 процесс' },
	{ name: '14 процесс' },
	{ name: '15 процесс' },
]

const ListProcess: FC<IListProcessProps> = ({ textForSearchProcess }) => {
	const dispatch = useAppDispatch()
	const openedProcess = useAppSelector(state => state.processes.openedProcess)

	const searchedListProcess = useMemo(
		() =>
			listProcess.filter(process =>
				process.name.toLowerCase().includes(textForSearchProcess.toLowerCase())
			),
		[textForSearchProcess, listProcess]
	)

	return (
		<List
			sx={{
				mt: 1,
				height: '100%',
				overflow: 'auto',
			}}
		>
			{!searchedListProcess.length && (
				<Typography
					variant='h4'
					sx={{ textAlign: 'center', fontFamily: 'Montserrat' }}
				>
					Процессов нет
				</Typography>
			)}
			{searchedListProcess.map((process, index) => (
				<ListItem
					key={index}
					className={(() =>
						openedProcess === process.name ? styles.openedProcessWrap : '')()}
				>
					<ListItemButton
						className={styles.openedProcess}
						onClick={() =>
							dispatch(changeOpenedProcess({ name: process.name }))
						}
					>
						<ListItemText>
							<Typography
								sx={{ color: '#333333' }}
								className={(() =>
									openedProcess === process.name
										? styles.openedProcessText
										: 'color: "#333333"')()}
							>
								{process.name}
							</Typography>
						</ListItemText>
					</ListItemButton>
				</ListItem>
			))}
		</List>
	)
}

export default ListProcess
