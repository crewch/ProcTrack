import { Box } from '@mui/material'
import styles from '/src/styles/StageForSuccessPageStyles/StageForSuccess.module.scss'
import SelectedProcess from './SelectedProcess/SelectedProcess'
import SelectedStage from './SelectedStage/SelectedStage'

const StageForSuccess = () => {
	return (
		<Box className={styles.container}>
			<SelectedProcess />
			<SelectedStage />
		</Box>
	)
}

export default StageForSuccess
