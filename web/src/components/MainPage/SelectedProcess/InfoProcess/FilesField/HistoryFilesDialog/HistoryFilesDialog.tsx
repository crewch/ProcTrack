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
import { FC, useState } from 'react'
import { IHistoryFilesDialogProps } from '../../../../../../interfaces/IMainPage/ISelectedProcess/IInfoProcess/IFilesField/IHistoryFilesDialog/IHistoryFilesDialog'
import styles from '/src/styles/MainPageStyles/SelectedProcessStyles/InfoProcessStyles/FilesFieldStyles/HistoryFilesDialogStyles/HistoryFilesDialog.module.scss'
import { fileApi } from '../../../../../../api/fileApi'

const HistoryFilesDialog: FC<IHistoryFilesDialogProps> = ({ passports }) => {
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
				<img src='/filesHistory.svg' height='20px' width='20px' />
			</IconButton>
			<Dialog
				className={styles.dialog}
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
										onClick={() => fileApi.getFile(passport.title)}
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

export default HistoryFilesDialog
