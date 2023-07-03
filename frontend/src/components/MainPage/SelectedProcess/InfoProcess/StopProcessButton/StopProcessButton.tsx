import { Box, Button } from '@mui/material'
import { FC } from 'react'
import styles from '/src/styles/MainPageStyles/SelectedProcessStyles/InfoProcessStyles/StopProcessButton/StopProcessButton.module.scss'

const StopProcessButton: FC = () => {
	return (
		<Box className={styles.container}>
			<Button
				className={styles.btn}
				variant='contained'
				component='button'
				endIcon={<img src='/src/assets/pause.svg' height='20px' width='20px' />}
			>
				Остановить процесс
			</Button>
		</Box>
	)
}

export default StopProcessButton
