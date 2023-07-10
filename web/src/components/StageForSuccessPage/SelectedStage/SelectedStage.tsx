import { Box, Divider } from '@mui/material'
import styles from '/src/styles/StageForSuccessPageStyles/SelectedStageStyles/SeclectedStage.module.scss'
import Header from './Header/Header'
import Body from './Body/Body'
import Footer from './Footer/Footer'

const SelectedStage = () => {
	return (
		<Box className={styles.container}>
			<Header />
			<Divider className={styles.divider} variant='middle' />
			<Body />
			<Divider className={styles.divider} variant='middle' />
			<Footer />
		</Box>
	)
}

export default SelectedStage
