import { Box, Divider } from '@mui/material'
import styles from '/src/styles/StageForSuccessPageStyles/SelectedProcessStyles/SelectedProcess.module.scss'
import Header from './Header/Header'
import Footer from './Footer/Footer'
import { useQuery } from '@tanstack/react-query'
import { useAppSelector } from '../../../hooks/reduxHooks'
import { getProcessApi } from '../../../api/getProcessApi'

const SelectedProcess = () => {
	const selectedStage = useAppSelector(state => state.processes.openedStage)

	const { data, isLoading, isSuccess } = useQuery({
		queryKey: ['processByStageIdStageForSuccess', selectedStage],
		queryFn: () => getProcessApi.getProcessByStageId(selectedStage),
	})

	return (
		<Box className={styles.container}>
			<Header
				selectedProcess={data}
				isLoading={isLoading}
				isSuccess={isSuccess}
			/>
			<Divider className={styles.divider} variant='middle' />
			<Footer
				selectedProcess={data}
				isLoading={isLoading}
				isSuccess={isSuccess}
			/>
		</Box>
	)
}

export default SelectedProcess
