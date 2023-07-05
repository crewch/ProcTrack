import { Box } from '@mui/material'
import AddProcessButton from './AddProcessButton/AddProcessButton'
import DataDialog from '../DataDialog/DataDialog'
import DataTable from '../../Dialogs/DataTable/DataTable'
import styles from '/src/styles/MainPageStyles/ContainerListProcessStyles/ButtonsStyles/Buttons.module.scss'

const Buttons = () => {
	return (
		<Box className={styles.btns}>
			<AddProcessButton />
			<DataDialog title='Tабличное представление' icon='table'>
				<DataTable />
			</DataDialog>
		</Box>
	)
}

export default Buttons
