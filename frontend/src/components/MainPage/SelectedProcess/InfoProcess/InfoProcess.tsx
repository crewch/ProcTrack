import { Box, Divider } from '@mui/material'
import { IInfoProcess } from '../../../../interfaces/IMainPage/ISelectedProcess/IInfoProcess/IInfoProcess'
import DateInfo from './DateInfoField/DateInfo'
import { ChangeEvent, useState } from 'react'
import UploadButton from './UploadButton/UploadButton'
import UserField from './UserField/UserField'
import HeaderProcessField from './HeaderProcessField/HeaderProcessField'
import FilesField from './FilesField/FilesField'
import styles from '/src/styles/MainPageStyles/SelectedProcessStyles/InfoProcessStyles/InfoProcess.module.scss'

const InfoProcess = () => {
	const process: IInfoProcess = {
		nameOfProcess: 'Первый процесс',
		statusOfProcess: 'в процессе',
		typeOfProcess: 'первый тип',
		importanceOfProcess: 'средняя важность',
		startDateOfProcess: 'пт, 22 декабря 2023 16:30',
		intervalOfProcess: '3 дня 2 часа 11 минут',
		responsible: 'Соколов Арсений',
		group: 'группа выпускающего',
	}

	const [file, setFile] = useState<File>()
	const handleFileChange = (e: ChangeEvent<HTMLInputElement>) => {
		if (e.target.files) {
			setFile(e.target.files[0])
		}
	}

	return (
		<Box className={styles.container}>
			<HeaderProcessField
				nameOfProcess={process.nameOfProcess}
				statusOfProcess={process.statusOfProcess}
				importanceOfProcess={process.importanceOfProcess}
				typeOfProcess={process.typeOfProcess}
			/>
			<Divider className={styles.divider} />
			<DateInfo
				startDate={process.startDateOfProcess}
				endData={'ср, 27 декабря 2023 12:00'}
				interval={process.intervalOfProcess}
			/>
			<Divider className={styles.divider} />
			<UserField responsible={process.responsible} group={process.group} />
			<Divider className={styles.divider} />
			<FilesField />
			<Divider className={styles.divider} />
			<UploadButton handleFileChange={handleFileChange} />
		</Box>
	)
}

export default InfoProcess
