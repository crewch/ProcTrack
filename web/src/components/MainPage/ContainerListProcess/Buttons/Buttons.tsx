import { Box } from '@mui/material'
import AddProcessButton from './AddProcessButton/AddProcessButton'
import DataDialog from '../DataDialog/DataDialog'
import DataTable from '../../Dialogs/DataTable/DataTable'
import styles from '/src/styles/MainPageStyles/ContainerListProcessStyles/ButtonsStyles/Buttons.module.scss'
import { FC } from 'react'
import { IContainerListProcessProps } from '../../../../interfaces/IMainPage/IContainerListProcess/IContainerListProcess'

const Buttons: FC<IContainerListProcessProps> = ({ page }) => {
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
}

export default Buttons
