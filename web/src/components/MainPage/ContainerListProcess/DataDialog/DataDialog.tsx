import Dialog from '@mui/material/Dialog'
import AppBar from '@mui/material/AppBar'
import Toolbar from '@mui/material/Toolbar'
import IconButton from '@mui/material/IconButton'
import Typography from '@mui/material/Typography'
import CloseIcon from '@mui/icons-material/Close'
import Slide from '@mui/material/Slide'
import { TransitionProps } from '@mui/material/transitions'
import { FC, forwardRef, memo, useState } from 'react'
import { Box, Divider } from '@mui/material'
import { IDialogProps } from '../../../../interfaces/IMainPage/IContainerListProcess/IDialogProps/IDialogProps'
import { CustomButton } from '../../../CustomButton/CustomButton'
import styles from '/src/styles/MainPageStyles/ContainerListProcessStyles/DataDialogStyles/DataDialog.module.scss'

const Transition = forwardRef(function Transition(
	props: TransitionProps & {
		children: React.ReactElement
	},
	ref: React.Ref<unknown>
) {
	return <Slide direction='up' ref={ref} {...props} />
})

const DataDialog: FC<IDialogProps> = memo(({ children, title, icon }) => {
	const [open, setOpen] = useState(false)

	const handleClickOpen = () => {
		setOpen(true)
	}

	const handleClose = () => {
		setOpen(false)
	}

	return (
		<>
			<CustomButton
				sx={{
					fontSize: {
						lg: '14px',
					},
					maxWidth: '53%',
				}}
				variant='contained'
				endIcon={<img src={`/${icon}.svg`} height='20px' width='20px' />}
				onClick={handleClickOpen}
			>
				{title}
			</CustomButton>
			<Dialog
				className={styles.dialog}
				fullScreen
				open={open}
				onClose={handleClose}
				TransitionComponent={Transition}
			>
				<AppBar className={styles.appBar}>
					<Toolbar className={styles.toolbar}>
						<IconButton
							edge='start'
							color='inherit'
							onClick={handleClose}
							aria-label='close'
						>
							<CloseIcon className={styles.icon} />
						</IconButton>
						<Typography className={styles.title} variant='h6' component='div'>
							{title}
						</Typography>
					</Toolbar>
				</AppBar>
				<Box className={styles.main} component='main'>
					<Divider className={styles.divider} />
					{children}
				</Box>
			</Dialog>
		</>
	)
})

export default DataDialog
