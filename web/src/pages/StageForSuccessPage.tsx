import { Box } from '@mui/material'
import styles from '/src/styles/MainPageStyles/MainPage.module.scss'
import { Navigate } from 'react-router-dom'
import ContainerListProcess from '../components/MainPage/ContainerListProcess/ContainerListProcess'
import StageForSuccess from '../components/StageForSuccessPage/StageForSuccess'

const StageForSuccessPage = () => {
	if (!localStorage.getItem('UserData')) {
		return <Navigate to='login' />
	}

	return (
		<Box component='main' className={styles.page}>
			<ContainerListProcess page='stageForSuccess' />
			<StageForSuccess />
		</Box>
	)
}

export default StageForSuccessPage
