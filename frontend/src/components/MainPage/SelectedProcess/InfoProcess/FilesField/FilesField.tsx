import { Box, Button, IconButton, Typography } from '@mui/material'
import styles from '/src/styles/MainPageStyles/SelectedProcessStyles/InfoProcessStyles/FilesFieldStyles/FilesField.module.scss'

const FilesField = () => {
	return (
		<Box className={styles.container}>
			<Button
				className={styles.btn}
				variant='contained'
				component='button'
				disableRipple
				startIcon={
					<img src='/src/assets/pdf-file.svg' height='20px' width='20px' />
				}
			>
				название файла 4.pdf
			</Button>
			<IconButton>
				<img src='/src/assets/comment.svg' height='20px' width='20px' />
			</IconButton>
			<IconButton>
				<img src='/src/assets/filesHistory.svg' height='20px' width='20px' />
			</IconButton>
			<Typography className={styles.typography}>
				пн, 25 декабря 2023, 16:32
			</Typography>
		</Box>
	)
}

export default FilesField
