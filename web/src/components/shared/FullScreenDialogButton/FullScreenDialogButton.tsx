import Dialog from '@mui/material/Dialog'
import AppBar from '@mui/material/AppBar'
import Toolbar from '@mui/material/Toolbar'
import IconButton from '@mui/material/IconButton'
import Typography from '@mui/material/Typography'
import CloseIcon from '@mui/icons-material/Close'
import Slide from '@mui/material/Slide'
import { TransitionProps } from '@mui/material/transitions'
import { FC, ReactNode, forwardRef, memo, useState } from 'react'
import { Box, Divider } from '@mui/material'
import { GrayButton } from '../../ui/button/GrayButton'
import styles from './FullScreenDialogButton.module.scss'

const Transition = forwardRef(function Transition(
	props: TransitionProps & {
		children: React.ReactElement
	},
	ref: React.Ref<unknown>
) {
	return <Slide direction='up' ref={ref} {...props} />
})

interface FullScreenDialogButtonProps {
	title: string
	icon: string
	children?: ReactNode
	fullWidth?: boolean
}

const FullScreenDialogButton: FC<FullScreenDialogButtonProps> = memo(
	({ children, title, icon, fullWidth }) => {
		const [open, setOpen] = useState(false)

		const handleClickOpen = () => {
			setOpen(true)
		}

		const handleClose = () => {
			setOpen(false)
		}

		return (
			<>
				<GrayButton
					fullWidth={fullWidth}
					sx={{
						fontSize: {
							lg: '0.875rem',
						},
						alignSelf: 'start',
						mt: '0.75rem',
					}}
					variant='contained'
					endIcon={
						<img src={`/${icon}.svg`} className={styles.grayButtonImg} />
					}
					onClick={handleClickOpen}
				>
					{title}
				</GrayButton>
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
							<Typography className={styles.title} variant='h6' component='h6'>
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
	}
)

export default FullScreenDialogButton
