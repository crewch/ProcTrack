import {
	List,
	ListItem,
	ListItemButton,
	ListItemText,
	Typography,
} from '@mui/material'
import { useAppDispatch, useAppSelector } from '../../../../hooks/reduxHooks'
import { changeOpenedProcess } from '../../../../store/processSlice/processSlice'
import { FC, useMemo } from 'react'
import { IListProcessProps } from '../../../../interfaces/IMainPage/IContainerListProcess/IListProcessProps'
import { IProcess } from '../../../../interfaces/IMainPage/IContainerListProcess/IListProcess'
import styles from '/src/styles/MainPageStyles/ContainerListProcessStyles/ListProcessStyles/ListProcess.module.scss'

const listProcess: IProcess[] = [
	{ name: '1 процесс', status: 'inprogress' },
	{ name: '2 процесс', status: 'completed' },
	{ name: '3 процесс', status: 'rejected' },
	{ name: '4 процесс', status: 'inprogress' },
	{ name: '5 процесс', status: 'completed' },
	{ name: '6 процесс', status: 'completed' },
	{ name: '7 процесс', status: 'inprogress' },
	{ name: '8 процесс', status: 'inprogress' },
	{ name: '9 процесс', status: 'inprogress' },
	{ name: '10 процесс', status: 'completed' },
	{ name: '11 процесс', status: 'rejected' },
	{ name: '12 процесс', status: 'inprogress' },
	{ name: '13 процесс', status: 'inprogress' },
	{ name: '14 процесс', status: 'completed' },
	{ name: '15 процесс', status: 'completed' },
]

const ListProcess: FC<IListProcessProps> = ({ textForSearchProcess }) => {
	const dispatch = useAppDispatch()
	const openedProcess = useAppSelector(state => state.processes.openedProcess)

	const searchedListProcess = useMemo(
		() =>
			listProcess.filter(process =>
				process.name.toLowerCase().includes(textForSearchProcess.toLowerCase())
			),
		[textForSearchProcess]
	)

	return (
		<List className={styles.list}>
			{!searchedListProcess.length && (
				<Typography variant='h4' className={styles.typography}>
					Процессов нет
				</Typography>
			)}
			{searchedListProcess.map((process, index) => (
				<ListItem
					key={index}
					sx={{ pl: 1 }}
					className={(() =>
						openedProcess === process.name ? styles.openedProcessWrap : '')()}
				>
					<img
						src={`src/assets/${process.status}.svg`}
						className={styles.img}
					/>
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
