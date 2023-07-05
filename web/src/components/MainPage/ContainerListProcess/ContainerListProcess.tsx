import { Box, Divider } from '@mui/material'
import { useState } from 'react'
import Search from './Search/Search'
import ListProccess from './ListProcess/ListProcess'
import SettingsList from './Search/SettingsList'
import Buttons from './Buttons/Buttons'
import styles from '/src/styles/MainPageStyles/ContainerListProcessStyles/ContainerListProcess.module.scss'

const ContainerListProcess = () => {
	const [isOpen, setIsOpen] = useState(false)
	const [textForSearchProcess, setTextForSearchProcess] = useState('')

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
					<ListProccess textForSearchProcess={textForSearchProcess} />
					<Buttons />
				</>
			)}
		</Box>
	)
}

export default ContainerListProcess
