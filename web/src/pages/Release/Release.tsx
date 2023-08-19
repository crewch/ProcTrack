import { Box } from '@mui/material'
import { Navigate } from 'react-router-dom'
import SearchList from '@/components/shared/SearchList/SearchList'
import SelectedProcessStagesList from '@/components/shared/SelectedProcessStagesList/SelectedProcessStagesList'
import SelectedStage from '@/components/shared/SelectedStage/SelectedStage'
import { getToken } from '@/utils/getToken'
import { FC } from 'react'
import styles from '@/shared/styles/page.module.scss'

interface ReleaseProps {
	socket: signalR.HubConnection
}

const Release: FC<ReleaseProps> = ({ socket }) => {
	if (!getToken()) {
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
