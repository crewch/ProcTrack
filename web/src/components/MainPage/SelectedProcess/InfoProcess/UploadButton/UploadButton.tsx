import { Box } from '@mui/material'
import { ChangeEventHandler, FC } from 'react'
import { CustomButton } from '../../../../CustomButton/CustomButton'

const UploadButton: FC<{
	handleFileChange: ChangeEventHandler<HTMLInputElement>
}> = ({ handleFileChange }) => {
	return (
		<Box component='label'>
			<CustomButton
				sx={{
					fontSize: {
						xs: '12px',
						lg: '14px',
					},
				}}
				component='span'
				variant='contained'
				endIcon={<img src='/folderUpload.svg' height='20px' width='20px' />}
			>
				Загрузить файл
			</CustomButton>
			<input hidden type='file' onChange={handleFileChange} />
		</Box>
	)
}

export default UploadButton
