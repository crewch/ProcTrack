import {
	Box,
	Dialog,
	DialogActions,
	DialogContent,
	DialogTitle,
	TextField,
} from '@mui/material'
import { ChangeEvent, FC, memo, useState } from 'react'
import { useMutation } from '@tanstack/react-query'
import TelegramIcon from '@mui/icons-material/Telegram'
import { GrayButton } from '@/components/ui/button/GrayButton'
import { passportService } from '@/services/passport'
import styles from './UploadButtonDialog.module.scss'
import { getUserData } from '@/utils/getUserData'

interface UploadButtonDialogProps {
	processId: number
}

const UploadButtonDialog: FC<UploadButtonDialogProps> = memo(
	({ processId }) => {
		const [message, setMessage] = useState('')
		const [file, setFile] = useState<FormData>()

		const userId = getUserData().id

		const mutation = useMutation({
			mutationFn: () =>
				passportService.sendPassport(userId, processId, file, message),
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
				<GrayButton
					sx={{
						fontSize: {
							xs: '0.75rem',
							lg: '0.875rem',
						},
					}}
					component='span'
					variant='contained'
					endIcon={
						<img src='/folderUpload.svg' className={styles.grayButtonImg} />
					}
					onClick={handleClickOpen}
				>
					Прикрепить паспорт
				</GrayButton>
				<Dialog
					PaperProps={{
						sx: {
							width: '40%',
							height: '30%',
							borderRadius: '1rem',
							p: '0.5rem',
						},
					}}
					open={open}
					onClose={handleClose}
				>
					<DialogTitle className={styles.dialogTitle}>
						Добавление паспорта
					</DialogTitle>
					<DialogContent className={styles.dialogContainer}>
						<Box className={styles.input}>
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
								className={styles.textField}
							/>
							<Box component='label' className={styles.upload}>
								<GrayButton
									sx={{
										height: '2.625rem',
										backgroundColor: file ? '#54C16C' : '',
										borderRadius: '0.5rem',
									}}
									component='span'
								>
									<img src='/folderUpload.svg' className={styles.uploadImg} />
								</GrayButton>
								<input hidden type='file' onChange={saveFile} />
							</Box>
						</Box>
					</DialogContent>
					<DialogActions>
						<GrayButton
							sx={{
								fontSize: {
									xs: '0.75rem',
									lg: '0.875rem',
								},
							}}
							disabled={!file || !message}
							component='span'
							variant='contained'
							endIcon={<TelegramIcon />}
							onClick={sendFile}
						>
							Отправить
						</GrayButton>
					</DialogActions>
				</Dialog>
			</>
		)
	}
)

export default UploadButtonDialog
