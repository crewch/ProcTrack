import { Box, Button } from '@mui/material'
import { ChangeEventHandler, FC } from 'react'

const UploadButton: FC<{
	handleFileChange: ChangeEventHandler<HTMLInputElement>
}> = ({ handleFileChange }) => {
	return (
		<Box
			component='label'
			sx={{
				borderRadius: '5px',
				width: '180px',
				height: '38px',
			}}
		>
			<Button
				sx={{
					backgroundColor: '#ECECEC',
					color: '#333333',
					width: '180px',
					fontSize: '14px',
					height: '38px',
					boxShadow: 'none',
					textTransform: 'none',
					'&:hover': {
						backgroundColor: 'transparent',
					},
				}}
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
