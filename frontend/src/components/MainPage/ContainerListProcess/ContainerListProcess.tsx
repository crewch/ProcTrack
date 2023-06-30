import { Box, Divider } from '@mui/material'
import { useState } from 'react'
import Search from './Search/Search'
import ListProccess from './ListProcess/ListProcess'
import GraphDialog from './GraphDialog/GraphDialog'
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
			<GraphDialog />
		</Box>
	)
}

export default ContainerListProcess
