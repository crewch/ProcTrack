import { Box } from '@mui/material'
import { Navigate } from 'react-router-dom'
import ContainerListProcess from '../../components/MainPage/ContainerListProcess/ContainerListProcess'
import StageForSuccess from '../../components/StageForSuccessPage/SelectedProcessAndListStage/SelectedProcessAndListStage'
import SelectedStage from '../../components/MainPage/SelectedStage/SelectedStage'
import styles from '../../shared/styles/page.module.scss'
import { useGetUserData } from '../../hooks/userDataHook'

const StageForSuccessPage = () => {
	if (!useGetUserData()) {
		return <Navigate to='/login' />
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
