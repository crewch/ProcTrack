import { Box } from '@mui/material'
import AddProcessButton from './AddProcessButton/AddProcessButton'
import DataDialog from '../DataDialog/DataDialog'
import DataTable from '../../Dialogs/DataTable/DataTable'
import { FC, memo } from 'react'
import { ISelectedStageProps } from '../../../../interfaces/IMainPage/ISelectedStage/ISelectedStage'
import styles from '/src/styles/MainPageStyles/ContainerListProcessStyles/ButtonsStyles/Buttons.module.scss'

const Buttons: FC<ISelectedStageProps> = memo(({ page }) => {
	return (
		<Box
			className={
				page === 'main' ? styles.mainPageBtns : styles.stageForSuccessPageBtns
			}
		>
			{page === 'main' && <AddProcessButton />}
			<DataDialog title='Tабличное представление' icon='table' page={page}>
				<DataTable />
			</DataDialog>
		</Box>
	)
})

export default Buttons
