import { Box, Button } from '@mui/material'
import styles from '/src/styles/MainPageStyles/ContainerListProcessStyles/DataDialogStyles/DataDialog.module.scss'

const AddProcessButton = () => {
	return (
		<Box className={styles.dataDialog}>
			<Button
				variant='contained'
				endIcon={
					<img src={`/src/assets/addProcess.svg`} height='20px' width='20px' />
				}
				className={styles.btn}
			>
				Добавить процесс
			</Button>
		</Box>
	)
}

export default AddProcessButton
