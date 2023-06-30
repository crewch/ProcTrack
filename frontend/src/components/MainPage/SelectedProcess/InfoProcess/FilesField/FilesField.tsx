import { Box, Button, IconButton, Typography } from '@mui/material'
import styles from '/src/styles/MainPageStyles/SelectedProcessStyles/InfoProcessStyles/FilesFieldStyles/FilesField.module.scss'

const FilesField = () => {
	return (
		<Box className={styles.container}>
			<Button
				className={styles.btn}
				variant='contained'
				component='button'
				startIcon={<img src='/src/assets/pdf-file.svg' />}
			>
				название файла 4.pdf
			</Button>
			<IconButton>
				<img src='/src/assets/comment.svg' />
			</IconButton>
			<IconButton>
				<img src='/src/assets/filesHistory.svg' />
			</IconButton>
			<Typography className={styles.typography}>
				пн, 25 декабря 2023, 16:32
			</Typography>
		</Box>
	)
}

export default FilesField
