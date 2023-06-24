import { Box } from '@mui/material'
import InfoProcess from './InfoProcess/InfoProcess'
import StagesList from './StagesList/StagesList'

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
			<StagesList />
		</Box>
	)
}

export default SelectedProcess
