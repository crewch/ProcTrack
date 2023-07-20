import { useState } from 'react'
import AddProcessDialog from './AddProcessDialog/AddProcessDialog'
import { GrayButton } from '../../../../ui/button/GrayButton'

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
						lg: '14px',
					},
				}}
				variant='contained'
				endIcon={<img src={`/addProcess.svg`} height='20px' width='20px' />}
				onClick={handleClickOpen}
			>
				Добавить процесс
			</GrayButton>
			<AddProcessDialog open={open} handleClose={handleClose} />
		</>
	)
}

export default AddProcessButton
