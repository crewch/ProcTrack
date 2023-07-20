import { useState } from 'react'
import { GrayButton } from '../../../../ui/button/GrayButton'
import AddProcess from '../../../Dialogs/AddProcess/AddProcess'

export default function FormDialog() {
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
						lg: '14px',
					},
				}}
				variant='contained'
				endIcon={<img src={`/addProcess.svg`} height='20px' width='20px' />}
				onClick={handleClickOpen}
			>
				Добавить процесс
			</GrayButton>
			<AddProcess open={open} handleClose={handleClose} />
		</>
	)
}
