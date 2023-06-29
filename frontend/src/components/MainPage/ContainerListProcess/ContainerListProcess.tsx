import { Box, Button, Divider } from '@mui/material'
import { useState } from 'react'
import Search from './Search/Search'
import ListProccess from './ListProcess/ListProcess'

const ContainerListProcess = () => {
	const [textForSearchProcess, setTextForSearchProcess] = useState('')

	return (
		<Box
			sx={{
				height: '100%',
				width: '25%',
				backgroundColor: 'white',
				borderRadius: '8px',
				p: 1,
				display: 'flex',
				flexDirection: 'column',
			}}
		>
			<Search
				textForSearchProcess={textForSearchProcess}
				setTextForSearchProcess={setTextForSearchProcess}
			/>
			<Divider variant='middle' sx={{ mb: 1, borderWidth: 1 }} />
			<ListProccess textForSearchProcess={textForSearchProcess} />
			<Button
				sx={{
					borderRadius: '5px',
					backgroundColor: '#ECECEC',
					color: '#333333',
					boxShadow: 'none',
					fontSize: '14px',
					textTransform: 'none',
					'&:hover': {
						backgroundColor: 'transparent',
					},
					alignSelf: 'start',
				}}
				variant='contained'
				endIcon={<img src='/src/assets/graph.svg' />}
			>
				графовое представление
			</Button>
		</Box>
	)
}

export default ContainerListProcess
