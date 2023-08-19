import { Box } from '@mui/material'
import { Navigate } from 'react-router-dom'
import SearchList from '@/components/shared/SearchList/SearchList'
import SelectedProcessStagesList from '@/components/shared/SelectedProcessStagesList/SelectedProcessStagesList'
import SelectedStage from '@/components/shared/SelectedStage/SelectedStage'
import { getToken } from '@/utils/getToken'
import styles from '@/shared/styles/page.module.scss'

const Approval = () => {
	if (!getToken()) {
		return <Navigate to='/login' />
	}

	return (
		<Box component='main' className={styles.page}>
			<SearchList page='approval' />
			<SelectedProcessStagesList page='approval' />
			<SelectedStage page='approval' />
		</Box>
	)
}

export default Approval
