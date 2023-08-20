import { Box, Button, Typography } from '@mui/material'
import { Navigate, useNavigate } from 'react-router-dom'
import styles from './NotFound.module.scss'
import { getUserData } from '@/utils/getUserData'

const NotFound = () => {
	const navigation = useNavigate()
	if (!getUserData()) {
		return <Navigate to='/login' />
	}

	return (
		<Box component='main' className={styles.main}>
			<img src='/notFound.svg' className={styles.imgNotFound} />
			<Typography component='h1' className={styles.typography}>
				Страница не найдена
			</Typography>
			<Button
				className={styles.btn}
				variant='contained'
				onClick={() => navigation('/')}
			>
				Вернуться на главную
			</Button>
		</Box>
	)
}

export default NotFound
