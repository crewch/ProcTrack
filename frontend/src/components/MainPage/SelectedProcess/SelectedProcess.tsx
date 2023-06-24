import { Box } from '@mui/material'
import InfoProcess from './InfoProcess/InfoProcess'

const SelectedProcess = () => {
	return (
		<Box
			sx={{
				height: '100%',
				width: '34%',
				borderRadius: '8px',
				display: 'flex',
				flexDirection: 'column',
				gap: 2,
			}}
		>
			<InfoProcess />
		</Box>
	)
}

export default SelectedProcess
