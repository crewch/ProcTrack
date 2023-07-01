import { Box, Divider } from '@mui/material'
import { ISelectedStage } from '../../../interfaces/IMainPage/ISelectedStage/ISelectedStage'
import styles from '/src/styles/MainPageStyles/SelectedStageStyles/SelectedStage.module.scss'
import UserField from '../SelectedProcess/InfoProcess/UserField/UserField'
import DateInfo from '../SelectedProcess/InfoProcess/DateInfoField/DateInfo'
import HeaderField from './HeaderField/HeaderField'

const SelectedStage = () => {
	const selectedStage: ISelectedStage = {
		name: 'Первый процесс',
		status: 'согласован с замечаниями',
		nameOfGroup: 'назначенная группа',
		role: 'Утверждающий',
		startDate: 'пт, 22 декабря 2023 16:30',
		endDate: 'пн, 25 декабря 2023 12:23',
		responsible: 'Сергей Сергеев',
		group: 'группа утверждающего',
	}

	return (
		<Box className={styles.selectedStage}>
			<HeaderField
				name={selectedStage.name}
				status={selectedStage.status}
				nameOfGroup={selectedStage.nameOfGroup}
			/>
			<Divider className={styles.divider} />
			<DateInfo
				startDate={selectedStage.startDate}
				endData={selectedStage.endDate}
			/>
			<Divider className={styles.divider} />
			<UserField
				group={selectedStage.group}
				responsible={selectedStage.responsible}
				role={selectedStage.role}
			/>
			<Divider className={styles.divider} />
		</Box>
	)
}

export default SelectedStage
