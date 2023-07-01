import Dialog from '@mui/material/Dialog'
import AppBar from '@mui/material/AppBar'
import Toolbar from '@mui/material/Toolbar'
import IconButton from '@mui/material/IconButton'
import Typography from '@mui/material/Typography'
import CloseIcon from '@mui/icons-material/Close'
import Slide from '@mui/material/Slide'
import { TransitionProps } from '@mui/material/transitions'
import { FC, forwardRef, useState } from 'react'
import Button from '@mui/material/Button'
// import stylesContainer from '/src/styles/MainPageStyles/ContainerListProcessStyles/ContainerListProcess.module.scss'
import styles from '/src/styles/MainPageStyles/ContainerListProcessStyles/DataDialog/DataDialog.module.scss'
import { Box, Divider } from '@mui/material'
import { IDialogProps } from '../../../../interfaces/IMainPage/IContainerListProcess/IDialogProps/IDialogProps'

const Transition = forwardRef(function Transition(
	props: TransitionProps & {
		children: React.ReactElement
	},
	ref: React.Ref<unknown>
) {
	return <Slide direction='up' ref={ref} {...props} />
})

const DataDialog: FC<IDialogProps> = ({ children, title, icon }) => {
	const [open, setOpen] = useState(false)

	const handleClickOpen = () => {
		setOpen(true)
	}

	const handleClose = () => {
		setOpen(false)
	}

	return (
		<Box>
			<Button
				variant='contained'
				endIcon={
					<img src={`/src/assets/${icon}.svg`} height='20px' width='20px' />
				}
				onClick={handleClickOpen}
				sx={{
					borderRadius: '5px',
					backgroundColor: '#ECECEC',
					color: '#333333',
					boxShadow: 'none',
					fontSize: '14px',
					textTransform: 'none',
					'&:hover': {
						backgroundColor: 'transparent',
					},
				}}
			>
				{title}
			</Button>
			<Dialog
				fullScreen
				open={open}
				onClose={handleClose}
				TransitionComponent={Transition}
			>
				<AppBar sx={{ position: 'relative', boxShadow: 'none' }}>
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
		</Box>
	)
}

export default DataDialog
