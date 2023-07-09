import { Box, Divider } from '@mui/material'
import { FC, useState } from 'react'
import Search from './Search/Search'
import ListProccess from './ListProcess/ListProcess'
import SettingsList from './Search/SettingsList'
import Buttons from './Buttons/Buttons'
import styles from '/src/styles/MainPageStyles/ContainerListProcessStyles/ContainerListProcess.module.scss'
import { IContainerListProcessProps } from '../../../interfaces/IMainPage/IContainerListProcess/IContainerListProcess'

const ContainerListProcess: FC<IContainerListProcessProps> = ({ page }) => {
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
					<Buttons page={page} />
				</>
			)}
		</Box>
	)
}

export default ContainerListProcess
