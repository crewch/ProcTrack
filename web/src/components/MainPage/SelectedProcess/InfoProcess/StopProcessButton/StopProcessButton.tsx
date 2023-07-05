import { FC } from 'react'
import { CustomButton } from '../../../../CustomButton/CustomButton'

const StopProcessButton: FC = () => {
	return (
		<CustomButton
			sx={{
				fontSize: {
					xs: '12px',
					lg: '14px',
				},
			}}
			variant='contained'
			endIcon={<img src='/pause.svg' height='20px' width='20px' />}
		>
			Остановить процесс
		</CustomButton>
	)
}

export default StopProcessButton
