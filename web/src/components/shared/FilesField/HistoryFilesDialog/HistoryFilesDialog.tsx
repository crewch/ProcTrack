import {
	Dialog,
	DialogContent,
	DialogTitle,
	IconButton,
	Link,
	List,
	ListItem,
	ListItemText,
	Tooltip,
	Typography,
} from '@mui/material'
import { FC, memo, useState } from 'react'
import { fileService } from '@/services/file'
import { Passport } from '@/shared/interfaces/passport'
import styles from './HistoryFilesDialog.module.scss'

interface HistoryFilesDialogProps {
	passports: Passport[]
}

const HistoryFilesDialog: FC<HistoryFilesDialogProps> = memo(
	({ passports }) => {
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
					<img src='/filesHistory.svg' className={styles.iconImg} />
				</IconButton>
				<Dialog
					className={styles.dialog}
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
					<DialogTitle className={styles.title}>
						Предыдущие версии паспортов
					</DialogTitle>
					<DialogContent>
						<List className={styles.list}>
							{!passports.length && (
								<Typography className={styles.infoText}>
									Предыдущих версий нет
								</Typography>
							)}
							{passports.map((passport, index) => (
								<ListItem divider className={styles.listItem} key={index}>
									<Tooltip arrow title={passport.title}>
										<Link
											component='button'
											onClick={() => fileService.getFile(passport.title)}
										>
											{passport.title.slice(0, 10)}
										</Link>
									</Tooltip>
									<ListItemText>{passport.createdAt}</ListItemText>
									<Tooltip arrow title={passport.message}>
										<ListItemText>{passport.message.slice(0, 10)}</ListItemText>
									</Tooltip>
								</ListItem>
							))}
						</List>
					</DialogContent>
				</Dialog>
			</>
		)
	}
)

export default HistoryFilesDialog
