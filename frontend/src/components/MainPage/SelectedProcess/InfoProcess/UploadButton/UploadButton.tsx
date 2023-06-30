import { Box, Button } from '@mui/material'
import { ChangeEventHandler, FC } from 'react'
import styles from '/src/styles/MainPageStyles/SelectedProcessStyles/InfoProcessStyles/UploadButtonStyles/UploadButton.module.scss'

const UploadButton: FC<{
	handleFileChange: ChangeEventHandler<HTMLInputElement>
}> = ({ handleFileChange }) => {
	return (
		<Box component='label' className={styles.container}>
			<Button
				className={styles.btn}
				variant='contained'
				component='span'
				endIcon={<img src='/src/assets/folderUpload.svg' />}
			>
				загрузить файл
			</Button>
			<input hidden type='file' onChange={handleFileChange} />
		</Box>
	)
}

export default UploadButton
