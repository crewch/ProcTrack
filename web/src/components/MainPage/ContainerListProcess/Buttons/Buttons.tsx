import { Box } from '@mui/material'
import AddProcessButton from './AddProcessButton/AddProcessButton'
import DataDialog from '../DataDialog/DataDialog'
import DataTable from '../../Dialogs/DataTable/DataTable'
import { FC, memo } from 'react'
import styles from '/src/styles/MainPageStyles/ContainerListProcessStyles/ButtonsStyles/Buttons.module.scss'

interface ButtonsProps {
	page?: 'main' | 'stageForSuccess'
}

const Buttons: FC<ButtonsProps> = memo(({ page }) => {
	return (
		<Box
			className={
				page === 'main' ? styles.mainPageBtns : styles.stageForSuccessPageBtns
			}
		>
			{page === 'main' && <AddProcessButton />}
			<DataDialog title='Tабличное представление' icon='table'>
				<DataTable />
			</DataDialog>
		</Box>
	)
})

export default Buttons
