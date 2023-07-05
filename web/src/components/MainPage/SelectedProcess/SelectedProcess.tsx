import { Box } from '@mui/material'
import InfoProcess from './InfoProcess/InfoProcess'
// import StagesList from './StagesList/StagesList'
import styles from '/src/styles/MainPageStyles/SelectedProcessStyles/SelectedProcess.module.scss'

const SelectedProcess = () => {
	return (
		<Box className={styles.selectedProcess}>
			<InfoProcess />
			{/* <StagesList /> */}
		</Box>
	)
}

export default SelectedProcess
