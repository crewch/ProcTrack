import { useState } from 'react'
import AddProcessDialog from './AddProcessDialog/AddProcessDialog'
import { GrayButton } from '@/components/ui/button/GrayButton'
import styles from './AddProcessButton.module.scss'

const AddProcessButton = () => {
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
				sx={{
					fontSize: {
						lg: '0.875rem',
					},
					mt: '0.75rem',
				}}
				variant='contained'
				endIcon={
					<img src={`/addProcess.svg`} className={styles.grayButtonImg} />
				}
				onClick={handleClickOpen}
			>
				Добавить процесс
			</GrayButton>
			<AddProcessDialog open={open} handleClose={handleClose} />
		</>
	)
}

export default AddProcessButton
