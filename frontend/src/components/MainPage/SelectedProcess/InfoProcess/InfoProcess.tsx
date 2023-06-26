import { Box, Divider } from '@mui/material'
import { IInfoProcess } from '../../../../interfaces/IMainPage/ISelectedProcess/IInfoProcess/IInfoProcess'
import DateInfo from './DateInfoField/DateInfo'
import { ChangeEvent, useState } from 'react'
import UploadButton from './UploadButton/UploadButton'
import UserField from './UserField/UserField'
import HeaderProcessField from './HeaderProcessField/HeaderProcessField'
import FilesField from './FilesField/FilesField'

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

	console.log(file)

	return (
		<Box
			sx={{
				height: '50%',
				width: '100%',
				backgroundColor: 'white',
				borderRadius: '8px',
				p: 2,
				display: 'flex',
				flexDirection: 'column',
			}}
		>
			<HeaderProcessField
				nameOfProcess={process.nameOfProcess}
				statusOfProcess={process.statusOfProcess}
				importanceOfProcess={process.importanceOfProcess}
				typeOfProcess={process.typeOfProcess}
			/>
			<Divider sx={{ my: 1, borderWidth: 1 }} />
			<DateInfo
				startDate={process.startDateOfProcess}
				endData={'ср, 27 декабря 2023 12:00'}
				interval={process.intervalOfProcess}
			/>
			<Divider sx={{ mb: 1, borderWidth: 1 }} />
			<UserField responsible={process.responsible} group={process.group} />
			<Divider sx={{ my: 1, borderWidth: 1 }} />
			<FilesField />
			<Divider sx={{ my: 1, borderWidth: 1 }} />
			<UploadButton handleFileChange={handleFileChange} />
		</Box>
	)
}

export default InfoProcess
