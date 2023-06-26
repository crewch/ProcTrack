import { Box } from '@mui/material'
import ContainerListProcess from '../components/MainPage/ContainerListProcess/ContainerListProcess'
import SelectedProcess from '../components/MainPage/SelectedProcess/SelectedProcess'
import { Navigate } from 'react-router-dom'
import SelectedStage from '../components/MainPage/SelectedStage/SelectedStage'

const MainPage = () => {
	if (!localStorage.getItem('TOKEN')) {
		return <Navigate to='login' />
	}

	return (
		<Box
			component='main'
			sx={{ height: '100%', width: '100%', p: 2, display: 'flex', gap: 2 }}
		>
			<ContainerListProcess />
			<SelectedProcess />
			<SelectedStage />
		</Box>
	)
}

export default MainPage
