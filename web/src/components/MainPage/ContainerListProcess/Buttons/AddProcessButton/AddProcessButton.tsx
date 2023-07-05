import { CustomButton } from '../../../../CustomButton/CustomButton'
import * as React from 'react'
import AddProcess from '../../../Dialogs/AddProcess/AddProcess'

export default function FormDialog() {
	const [open, setOpen] = React.useState(false)

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
				}}
				variant='contained'
				endIcon={<img src={`/addProcess.svg`} height='20px' width='20px' />}
				onClick={handleClickOpen}
			>
				Добавить процесс
			</CustomButton>
			<AddProcess open={open} handleClose={handleClose} />
		</>
	)
}
