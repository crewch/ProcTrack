import { Box } from '@mui/material'
import { CustomButton } from '../../../../CustomButton/CustomButton'
import { ChangeEvent, FC, useState } from 'react'
import { passportApi } from '../../../../../api/passportApi'
import { IUploadButtonProps } from '../../../../../interfaces/IMainPage/ISelectedProcess/IInfoProcess/IUploadButton/IUploadButton'
import { useMutation, useQueryClient } from '@tanstack/react-query'

const UploadButton: FC<IUploadButtonProps> = ({ processId }) => {
	const [message, _setMessage] = useState('') //TODO: добавить ввод сообщения
	const [file, setFile] = useState<FormData>()

	const queryClient = useQueryClient()
	const mutation = useMutation({
		mutationFn: () => passportApi.sendFilePasport(processId, file, message),
		onSuccess: () => {
			queryClient.invalidateQueries({ queryKey: ['passport'] })
		},
	})

	const sendFile = (e: ChangeEvent<HTMLInputElement>) => {
		if (e && e?.target && e.target?.files) {
			const formData = new FormData()
			formData.append('file', e.target.files[0])
			setFile(formData)

			mutation.mutate()
		}
	}

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
			<input hidden type='file' onChange={sendFile} />
		</Box>
	)
}

export default UploadButton
