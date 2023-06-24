import { Box } from '@mui/material'
import ContainerListProcess from '../components/MainPage/ContainerListProcess/ContainerListProcess'
import SelectedProcess from '../components/MainPage/SelectedProcess/SelectedProcess'

const MainPage = () => {
	return (
		<Box sx={{ height: '100%', width: '100%', p: 2, display: 'flex', gap: 2 }}>
			<ContainerListProcess />
			<SelectedProcess />
		</Box>
	)
}

export default MainPage
