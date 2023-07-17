import { Box, Divider } from '@mui/material'
import { FC, useState } from 'react'
import Search from './Search/Search'
import ListProccess from './ListProcess/ListProcess'
import SettingsList from './Search/SettingsList'
import Buttons from './Buttons/Buttons'
import StagesList from '../../StageForSuccessPage/SelectedStage/StageList/StagesList'
import { ISelectedStageProps } from '../../../interfaces/IMainPage/ISelectedStage/ISelectedStage'
import styles from '/src/styles/MainPageStyles/ContainerListProcessStyles/ContainerListProcess.module.scss'

const ContainerListProcess: FC<ISelectedStageProps> = ({ page }) => {
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
					{page === 'main' && (
						<ListProccess textForSearchProcess={textForSearchProcess} />
					)}
					{page === 'stageForSuccess' && (
						<StagesList textForSearchProcess={textForSearchProcess} />
					)}
					<Buttons page={page} />
				</>
			)}
		</Box>
	)
}

export default ContainerListProcess
