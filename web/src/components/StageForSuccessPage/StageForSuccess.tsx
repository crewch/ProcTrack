import { Box } from '@mui/material'
import SelectedProcess from './SelectedProcess/SelectedProcess'
import StagesList from '../MainPage/SelectedProcess/StagesList/StagesList'
import styles from '/src/styles/StageForSuccessPageStyles/StageForSuccess.module.scss'

const StageForSuccess = () => {
	return (
		<Box className={styles.container}>
			<SelectedProcess />
			<StagesList page='stageForSuccess' />
		</Box>
	)
}

export default StageForSuccess
