import {
	Box,
	Dialog,
	DialogActions,
	DialogContent,
	DialogTitle,
	TextField,
} from '@mui/material'
import { GrayButton } from '../../../../ui/button/GrayButton'
import { ChangeEvent, FC, memo, useState } from 'react'
import { useMutation, useQueryClient } from '@tanstack/react-query'
import TelegramIcon from '@mui/icons-material/Telegram'
import { passportService } from '../../../../../services/passport'
import { useGetUserData } from '../../../../../hooks/userDataHook'
import styles from './UploadButtonDialog.module.scss'

interface UploadButtonDialogProps {
	processId: number
}

const UploadButtonDialog: FC<UploadButtonDialogProps> = memo(
	({ processId }) => {
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
				<GrayButton
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
				</GrayButton>
				<Dialog
					PaperProps={{
						sx: {
							width: '40%',
							height: '30%',
							borderRadius: '16px',
							p: 1,
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
										height: '42px',
										backgroundColor: file ? '#54C16C' : '',
										borderRadius: '8px',
									}}
									component='span'
								>
									<img src='/folderUpload.svg' height='25px' width='25px' />
								</GrayButton>
								<input hidden type='file' onChange={saveFile} />
							</Box>
						</Box>
					</DialogContent>
					<DialogActions>
						<GrayButton
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
						</GrayButton>
					</DialogActions>
				</Dialog>
			</>
		)
	}
)

export default UploadButtonDialog
