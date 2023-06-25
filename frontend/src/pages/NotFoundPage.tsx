import { Box, Button, Typography } from '@mui/material'
import NotFound from '../assets/notFound.svg'
import { useNavigate } from 'react-router-dom'
import { useEffect } from 'react'

const NotFoundPage = () => {
	const navigation = useNavigate()
	useEffect(() => {
		if (!localStorage.getItem('TOKEN')) {
			navigation('/login')
		}
	}, [navigation])

	return (
		<Box
			component='main'
			sx={{
				height: '100%',
				display: 'flex',
				flexDirection: 'column',
				justifyContent: 'center',
				alignItems: 'center',
			}}
		>
			<img src={NotFound} height='421.19px' width='833px' />
			<Typography
				component='h1'
				sx={{
					color: '#333333',
					fontFamily: 'Montserrat',
					fontWeight: 600,
					m: 1,
					fontSize: '64px',
				}}
			>
				Страница не найдена
			</Typography>
			<Button
				sx={{
					m: 2,
					width: '307px',
					height: '104px',
					borderRadius: '13px',
					backgroundColor: '#5C98D0',
				}}
				variant='contained'
				onClick={() => navigation('/')}
			>
				Вернуться на главную
			</Button>
		</Box>
	)
}

export default NotFoundPage
