import { Box, Divider } from '@mui/material'
import { useState } from 'react'
import Search from './Search/Search'
import ListProccess from './ListProcess/ListProcess'
import DataDialog from './DataDialog/DataDialog'
import styles from '/src/styles/MainPageStyles/ContainerListProcessStyles/ContainerListProcess.module.scss'
import DataGraph from '../Dialogs/DataGraph/DataGraph'
import SettingsList from './Search/SettingsList'

const ContainerListProcess = () => {
	const [textForSearchProcess, setTextForSearchProcess] = useState('')
	const [isOpen, setIsOpen] = useState(false)

	return (
		<Box className={styles.containerListProcess}>
			<Search
				isOpen={isOpen}
				setIsOpen={setIsOpen}
				textForSearchProcess={textForSearchProcess}
				setTextForSearchProcess={setTextForSearchProcess}
			/>
			<Divider variant='middle' className={styles.divider} />
			{isOpen ? (
				<SettingsList />
			) : (
				<>
					<ListProccess />
					<DataDialog title='Графовое представление' icon='graph'>
						<DataGraph />
					</DataDialog>
				</>
			)}
		</Box>
	)
}

export default ContainerListProcess
