import { Box, Divider } from '@mui/material'
import styles from '/src/styles/StageForSuccessPageStyles/SelectedStageStyles/SeclectedStage.module.scss'
import Header from './Header/Header'
import Body from './Body/Body'
import Footer from './Footer/Footer'
import { useQuery } from '@tanstack/react-query'
import { getStageApi } from '../../../api/getStageApi'
import { useAppSelector } from '../../../hooks/reduxHooks'

const SelectedStage = () => {
	const stageId = useAppSelector(state => state.processes.openedStage)

	const {
		data: selectedStage,
		isLoading,
		isSuccess,
	} = useQuery({
		queryKey: ['selectedStageStageForSuccessPage', stageId],
		queryFn: () => getStageApi.getStageId(stageId),
	})

	return (
		<Box className={styles.container}>
			<Header
				selectedStage={selectedStage}
				isLoading={isLoading}
				isSuccess={isSuccess}
			/>
			<Divider className={styles.divider} variant='middle' />
			<Body
				selectedStage={selectedStage}
				isLoading={isLoading}
				isSuccess={isSuccess}
			/>
			<Divider className={styles.divider} variant='middle' />
			<Footer
				selectedStage={selectedStage}
				isLoading={isLoading}
				isSuccess={isSuccess}
			/>
		</Box>
	)
}

export default SelectedStage
