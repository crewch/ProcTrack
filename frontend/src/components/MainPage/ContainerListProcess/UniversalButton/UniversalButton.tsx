import { Box, Button } from '@mui/material'
import { FC, ReactNode } from 'react'
import styles from '/src/styles/MainPageStyles/ContainerListProcessStyles/DataDialogStyles/DataDialog.module.scss'

interface IUButton {
	children: ReactNode
	icon: string
	position: 'start' | 'end'
}

const UniversalButton: FC<IUButton> = ({ children, icon, position }) => {
	if (position === 'start') {
		return (
			<Box className={styles.container}>
				<Button
					variant='contained'
					startIcon={
						<img src={`/src/assets/${icon}.svg`} height='20px' width='20px' />
					}
					className={styles.btn}
				>
					{children}
				</Button>
			</Box>
		)
	} else {
		return (
			<Box className={styles.container}>
				<Button
					variant='contained'
					endIcon={
						<img src={`/src/assets/${icon}.svg`} height='20px' width='20px' />
					}
					className={styles.btn}
				>
					{children}
				</Button>
			</Box>
		)
	}
}

export default UniversalButton
