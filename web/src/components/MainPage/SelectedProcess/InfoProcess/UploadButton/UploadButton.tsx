import {
	Box,
	Dialog,
	DialogActions,
	DialogContent,
	DialogTitle,
	TextField,
} from '@mui/material'
import { CustomButton } from '../../../../CustomButton/CustomButton'
import { ChangeEvent, FC, memo, useState } from 'react'
import { useMutation, useQueryClient } from '@tanstack/react-query'
import TelegramIcon from '@mui/icons-material/Telegram'
import { passportService } from '../../../../../services/passport'
import styles from '/src/styles/MainPageStyles/SelectedProcessStyles/InfoProcessStyles/UploadButtonStyles/UploadButton.module.scss'
import { useGetUserData } from '../../../../../hooks/userDataHook'

interface UploadButtonProps {
	processId: number
}

const UploadButton: FC<UploadButtonProps> = memo(({ processId }) => {
	const [message, setMessage] = useState('')
	const [file, setFile] = useState<FormData>()

	const userId = useGetUserData().id

	const queryClient = useQueryClient()
	const mutation = useMutation({
		mutationFn: () =>
			passportService.sendPassport(userId, processId, file, message),
		onSuccess: () => {
			queryClient.invalidateQueries({ queryKey: ['passport'] })
		},
	})

	const saveFile = (e: ChangeEvent<HTMLInputElement>) => {
		if (e && e?.target && e.target?.files) {
			const formData = new FormData()
			formData.append('file', e.target.files[0])
			setFile(formData)

			e.target.value = ''
		}
	}

	const [open, setOpen] = useState(false)

	const handleClickOpen = () => {
		setOpen(true)
	}

	const handleClose = () => {
		setOpen(false)
		setFile(undefined)
		setMessage('')
	}

	const sendFile = async () => {
		await mutation.mutate()

		handleClose()
	}

	return (
		<>
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
				onClick={handleClickOpen}
			>
				Прикрепить паспорт
			</CustomButton>

			<Dialog
				PaperProps={{
					sx: {
						width: '40%',
						height: '40%',
						borderRadius: '16px',
						p: 1,
					},
				}}
				open={open}
				onClose={handleClose}
			>
				<DialogTitle>Добавление паспорта</DialogTitle>
				<DialogContent className={styles.dialogContainer}>
					<TextField
						value={message}
						onChange={e => setMessage(e.target.value)}
						placeholder='Введите сообщение'
						autoComplete='off'
						variant='standard'
						InputProps={{
							className: styles.InputProps,
							disableUnderline: true,
						}}
						className={styles.TextField}
					></TextField>
					<Box component='label' className={styles.upload}>
						<CustomButton
							sx={{
								fontSize: {
									xs: '12px',
									lg: '14px',
								},
								backgroundColor: file ? '#54C16C' : '',
							}}
							component='span'
							variant='contained'
							endIcon={
								<img src='/folderUpload.svg' height='20px' width='20px' />
							}
						>
							Прикрепить паспорт
						</CustomButton>
						<input hidden type='file' onChange={saveFile} />
					</Box>
				</DialogContent>
				<DialogActions>
					<CustomButton
						sx={{
							fontSize: {
								xs: '12px',
								lg: '14px',
							},
						}}
						disabled={!file || !message}
						component='span'
						variant='contained'
						endIcon={<TelegramIcon />}
						onClick={sendFile}
					>
						Отправить
					</CustomButton>
				</DialogActions>
			</Dialog>
		</>
	)
})

export default UploadButton
