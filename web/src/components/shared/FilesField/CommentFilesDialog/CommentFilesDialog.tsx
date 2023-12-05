import {
	Dialog,
	DialogContent,
	DialogTitle,
	IconButton,
	Typography,
} from '@mui/material'
import { FC, memo, useState } from 'react'
import styles from './CommentFilesDialog.module.scss'

interface CommentFilesDialogProps {
	message: string
}

const CommentFilesDialog: FC<CommentFilesDialogProps> = memo(({ message }) => {
	const [open, setOpen] = useState(false)

	const handleClickOpen = () => {
		setOpen(true)
	}

	const handleClose = () => {
		setOpen(false)
	}

	return (
		<>
			<IconButton onClick={handleClickOpen}>
				<img src='/comment.svg' className={styles.iconImg} />
			</IconButton>
			<Dialog
				PaperProps={{
					sx: {
						width: '40%',
						height: '50%',
						borderRadius: '1rem',
					},
				}}
				open={open}
				onClose={handleClose}
			>
				<DialogTitle className={styles.title}>Комментарий</DialogTitle>
				<DialogContent>
					<Typography>{message}</Typography>
				</DialogContent>
			</Dialog>
		</>
	)
})

export default CommentFilesDialog
