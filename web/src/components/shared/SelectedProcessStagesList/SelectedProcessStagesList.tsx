import { Box, Button } from '@mui/material'
import StagesList from '../StagesList/StagesList'
import { FC, useState } from 'react'
import { useAppSelector } from '@/hooks/reduxHooks'
import InfoProcessApproval from './InfoProcessApproval/InfoProcessApproval'
import InfoProcessRelease from './InfoProcessRelease/InfoProcessRelease'
import styles from './SelectedProcessStagesList.module.scss'

interface SelectedProcessStagesListProps {
	page: 'release' | 'approval'
}

const SelectedProcessStagesList: FC<SelectedProcessStagesListProps> = ({
	page,
}) => {
	const [isOpen, setIsOpen] = useState(false)

	const selectedProcess = useAppSelector(
		state => state.processStage.openedProcess
	)
	const selectedStage = useAppSelector(state => state.processStage.openedStage)

	return (
		<Box className={styles.container}>
			{isOpen ? (
				<StagesList />
			) : (
				<>
					{page === 'release' && <InfoProcessRelease />}
					{page === 'approval' && <InfoProcessApproval />}
				</>
			)}
			{(selectedProcess || selectedStage) && (
				<Button
					onClick={() => setIsOpen(!isOpen)}
					sx={{ backgroundColor: 'white' }}
					className='text-ap-black'
				>
					{isOpen ? 'Закрыть список этапов' : 'Открыть список этапов'}
				</Button>
			)}
		</Box>
	)
}

export default SelectedProcessStagesList
