import { Box } from '@mui/material'
import { Navigate } from 'react-router-dom'
import ContainerListProcess from '../components/MainPage/ContainerListProcess/ContainerListProcess'
import StageForSuccess from '../components/StageForSuccessPage/StageForSuccess'
import SelectedStage from '../components/MainPage/SelectedStage/SelectedStage'
import styles from '/src/styles/MainPageStyles/MainPage.module.scss'

const StageForSuccessPage = () => {
	if (!localStorage.getItem('UserData')) {
		return <Navigate to='login' />
	}

	return (
		<Box component='main' className={styles.page}>
			<ContainerListProcess page='stageForSuccess' />
			<StageForSuccess />
			<SelectedStage page='stageForSuccess' />
		</Box>
	)
}

export default StageForSuccessPage
