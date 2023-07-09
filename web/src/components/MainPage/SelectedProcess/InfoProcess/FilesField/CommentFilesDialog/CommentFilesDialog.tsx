import {
	Dialog,
	DialogContent,
	DialogTitle,
	IconButton,
	Typography,
} from '@mui/material'
import { FC, useState } from 'react'
import { ICommentFilesDialogProps } from '../../../../../../interfaces/IMainPage/ISelectedProcess/IInfoProcess/IFilesField/ICommentFilesDialog/ICommentFilesDialog'

const CommentFilesDialog: FC<ICommentFilesDialogProps> = ({ message }) => {
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
				<img src='/comment.svg' height='20px' width='20px' />
			</IconButton>
			<Dialog
				PaperProps={{
					sx: {
						width: '40%',
						height: '50%',
						borderRadius: '16px',
					},
				}}
				open={open}
				onClose={handleClose}
			>
				<DialogTitle>Комментарий</DialogTitle>
				<DialogContent>
					<Typography>{message}</Typography>
				</DialogContent>
			</Dialog>
		</>
	)
}

export default CommentFilesDialog
