import { Box } from '@mui/material'
import StagesList from '../StagesList/StagesList'
import { FC } from 'react'
import InfoProcessApproval from '../../screens/Approval/InfoProcess/InfoProcessApproval'
import InfoProcessRelease from '../../screens/Release/InfoProcess/InfoProcessRelease'
import styles from './SelectedProcessStagesList.module.scss'

interface SelectedProcessStagesListProps {
	page: 'release' | 'approval'
}

const SelectedProcessStagesList: FC<SelectedProcessStagesListProps> = ({
	page,
}) => {
	return (
		<Box className={styles.container}>
			{page === 'release' && <InfoProcessRelease />}
			{page === 'approval' && <InfoProcessApproval />}
			<StagesList />
		</Box>
	)
}

export default SelectedProcessStagesList
