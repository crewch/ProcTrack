import { Box } from '@mui/material'
import { Navigate } from 'react-router-dom'
import ContainerListProcess from '../../components/MainPage/ContainerListProcess/ContainerListProcess'
import SelectedProcess from '../../components/MainPage/SelectedProcess/SelectedProcess'
import SelectedStage from '../../components/MainPage/SelectedStage/SelectedStage'
import { useGetUserData } from '../../hooks/userDataHook'
import styles from '../../shared/styles/page.module.scss'

const Release = () => {
	if (!useGetUserData()) {
		return <Navigate to='login' />
	}

	return (
		<Box component='main' className={styles.page}>
			<ContainerListProcess page='main' />
			<SelectedProcess />
			<SelectedStage page='main' />
		</Box>
	)
}

export default Release
