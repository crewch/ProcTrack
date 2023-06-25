import { Box } from '@mui/material'
import ContainerListProcess from '../components/MainPage/ContainerListProcess/ContainerListProcess'
import SelectedProcess from '../components/MainPage/SelectedProcess/SelectedProcess'
import { useNavigate } from 'react-router-dom'
import { useEffect } from 'react'

const MainPage = () => {
	const navigation = useNavigate()
	useEffect(() => {
		if (!localStorage.getItem('TOKEN')) {
			navigation('login')
		}
	}, [navigation])

	return (
		<Box
			component='main'
			sx={{ height: '100%', width: '100%', p: 2, display: 'flex', gap: 2 }}
		>
			<ContainerListProcess />
			<SelectedProcess />
		</Box>
	)
}

export default MainPage
