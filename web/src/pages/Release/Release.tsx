import { Box } from '@mui/material'
import { Navigate } from 'react-router-dom'
import SearchList from '@/components/shared/SearchList/SearchList'
import SelectedProcessStagesList from '@/components/shared/SelectedProcessStagesList/SelectedProcessStagesList'
import SelectedStage from '@/components/shared/SelectedStage/SelectedStage'
import styles from '@/shared/styles/page.module.scss'
import { getUserData } from '@/utils/getUserData'

const Release = () => {
	if (!getUserData()) {
		return <Navigate to='login' />
	}

	return (
		<Box component='main' className={styles.page}>
			<SearchList page='release' />
			<SelectedProcessStagesList page='release' />
			<SelectedStage page='release' />
		</Box>
	)
}

export default Release
