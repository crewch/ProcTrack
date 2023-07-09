import { Box, Divider } from '@mui/material'
import styles from '/src/styles/StageForSuccessPageStyles/SelectedProcessStyles/SelectedProcess.module.scss'
import Header from './Header/Header'
import Footer from './Footer/Footer'

const SelectedProcess = () => {
	return (
		<Box className={styles.container}>
			<Header />
			<Divider className={styles.divider} variant='middle' />
			<Footer />
		</Box>
	)
}

export default SelectedProcess
