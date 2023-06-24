import { Box } from '@mui/material'
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
			<ListProccess textForSearchProcess={textForSearchProcess} />
		</Box>
	)
}

export default ContainerListProcess
