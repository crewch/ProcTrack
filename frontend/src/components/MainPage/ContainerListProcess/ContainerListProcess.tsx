import { Box, Button, Divider } from '@mui/material'
import { useState } from 'react'
import Search from './Search/Search'
import ListProccess from './ListProcess/ListProcess'
import styles from '/src/styles/MainPageStyles/ContainerListProcessStyles/ContainerListProcess.module.scss'

const ContainerListProcess = () => {
	const [textForSearchProcess, setTextForSearchProcess] = useState('')

	return (
		<Box className={styles.containerListProcess}>
			<Search
				textForSearchProcess={textForSearchProcess}
				setTextForSearchProcess={setTextForSearchProcess}
			/>
			<Divider variant='middle' className={styles.divider} />
			<ListProccess textForSearchProcess={textForSearchProcess} />
			<Button
				className={styles.btn}
				variant='contained'
				endIcon={<img src='/src/assets/graph.svg' />}
			>
				графовое представление
			</Button>
		</Box>
	)
}

export default ContainerListProcess
