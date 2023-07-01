import { Box, Divider } from '@mui/material'
import { useState } from 'react'
import Search from './Search/Search'
import ListProccess from './ListProcess/ListProcess'
import DataDialog from './DataDialog/DataDialog'
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
			<DataDialog title='Графовое представление' icon='graph' />
		</Box>
	)
}

export default ContainerListProcess
