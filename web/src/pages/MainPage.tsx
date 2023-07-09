import { Box } from '@mui/material'
import { Navigate } from 'react-router-dom'
import ContainerListProcess from '../components/MainPage/ContainerListProcess/ContainerListProcess'
import SelectedProcess from '../components/MainPage/SelectedProcess/SelectedProcess'
import SelectedStage from '../components/MainPage/SelectedStage/SelectedStage'
import styles from '../styles/MainPageStyles/MainPage.module.scss'

const MainPage = () => {
	if (!localStorage.getItem('UserData')) {
		return <Navigate to='login' />
	}

	return (
		<Box component='main' className={styles.page}>
			<ContainerListProcess />
			<SelectedProcess />
			<SelectedStage />
		</Box>
	)
}

export default MainPage
