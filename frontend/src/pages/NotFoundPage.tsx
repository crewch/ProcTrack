import { Box, Button, Typography } from '@mui/material'
import { Navigate, useNavigate } from 'react-router-dom'
import styles from '../styles/NotFoundPageStyles/NotFoundPage.module.scss'

const NotFoundPage = () => {
	const navigation = useNavigate()
	if (!localStorage.getItem('UserData')) {
		return <Navigate to='login' />
	}

	return (
		<Box component='main' className={styles.main}>
			<img src='/src/assets/notFound.svg' className={styles.imgNotFound} />
			<Typography component='h1' className={styles.typography}>
				Страница не найдена
			</Typography>
			<Button
				disableRipple
				className={styles.btn}
				variant='contained'
				onClick={() => navigation('/')}
			>
				Вернуться на главную
			</Button>
		</Box>
	)
}

export default NotFoundPage
