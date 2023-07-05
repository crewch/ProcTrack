import { CustomButton } from '../../../../CustomButton/CustomButton'

const AddProcessButton = () => {
	return (
		<CustomButton
			sx={{
				fontSize: {
					lg: '14px',
				},
			}}
			variant='contained'
			endIcon={<img src={`/addProcess.svg`} height='20px' width='20px' />}
		>
			Добавить процесс
		</CustomButton>
	)
}

export default AddProcessButton
