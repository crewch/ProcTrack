import { Box } from '@mui/material'
import styles from '/src/styles/MainPageStyles/MainPage.module.scss'
import { Navigate } from 'react-router-dom'

const StageForSuccessPage = () => {
	if (!localStorage.getItem('UserData')) {
		return <Navigate to='login' />
	}

	return (
		<Box component='main' className={styles.page}>
			StageForSuccessPage
		</Box>
	)
}

export default StageForSuccessPage
