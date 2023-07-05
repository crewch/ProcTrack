import { Box, Divider } from '@mui/material'
import { IInfoProcess } from '../../../../interfaces/IMainPage/ISelectedProcess/IInfoProcess/IInfoProcess'
import DateInfo from './DateInfoField/DateInfo'
import { ChangeEvent, useState } from 'react'
import UploadButton from './UploadButton/UploadButton'
import UserField from './UserField/UserField'
import HeaderField from './HeaderProcessField/HeaderField'
import FilesField from './FilesField/FilesField'
import styles from '/src/styles/MainPageStyles/SelectedProcessStyles/InfoProcessStyles/InfoProcess.module.scss'
import StopProcessButton from './StopProcessButton/StopProcessButton'

const InfoProcess = () => {
	const process: IInfoProcess = {
		name: 'Первый процесс',
		status: 'в процессе',
		type: 'первый тип',
		importance: 'средняя важность',
		startDate: 'пт, 22 декабря 2023 16:30',
		interval: '3 дня 2 часа 11 минут',
		responsible: 'Соколов Арсений',
		group: 'группа выпускающего',
		role: 'Ответственный',
	}

	const [_file, setFile] = useState<File>()
	const handleFileChange = (e: ChangeEvent<HTMLInputElement>) => {
		if (e.target.files) {
			setFile(e.target.files[0])
		}
	}

	return (
		<Box className={styles.container}>
			<HeaderField
				name={process.name}
				status={process.status}
				importance={process.importance}
				type={process.type}
			/>
			<Divider className={styles.divider} />
			<DateInfo
				startDate={process.startDate}
				endData={'ср, 27 декабря 2023 12:00'}
				interval={process.interval}
			/>
			<Divider className={styles.divider} />
			<UserField
				responsible={process.responsible}
				group={process.group}
				role={process.role}
			/>
			<Divider className={styles.divider} />
			<FilesField />
			<Divider className={styles.divider} />
			<Box className={styles.btns}>
				<StopProcessButton />
				<UploadButton handleFileChange={handleFileChange} />
			</Box>
		</Box>
	)
}

export default InfoProcess
