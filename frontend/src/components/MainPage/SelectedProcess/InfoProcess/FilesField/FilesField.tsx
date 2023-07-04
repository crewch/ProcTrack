import { Box, IconButton, Typography } from '@mui/material'
import { CustomButton } from '../../../../CustomButton/CustomButton'
import styles from '/src/styles/MainPageStyles/SelectedProcessStyles/InfoProcessStyles/FilesFieldStyles/FilesField.module.scss'

const FilesField = () => {
	return (
		<Box className={styles.container}>
			<CustomButton
				sx={{
					fontSize: {
						xs: '12px',
						lg: '14px',
					},
				}}
				variant='contained'
				startIcon={<img src='pdf-file.svg' height='20px' width='20px' />}
			>
				название файла 4.pdf
			</CustomButton>
			<IconButton>
				<img src='/comment.svg' height='20px' width='20px' />
			</IconButton>
			<IconButton>
				<img src='/filesHistory.svg' height='20px' width='20px' />
			</IconButton>
			<Typography className={styles.typography}>
				пн, 25 декабря 2023, 16:32
			</Typography>
		</Box>
	)
}

export default FilesField
