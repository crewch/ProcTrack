import { Box } from '@mui/material'
import { ISelectedStage } from '../../../interfaces/IMainPage/ISelectedStage/ISelectedStage'

const SelectedStage = () => {
	const selectedStage: ISelectedStage = {
		name: 'Первый процесс',
		status: 'в процессе',
		type: 'первый тип',
		importance: 'средняя важность',
		startDate: 'пт, 22 декабря 2023 16:30',
		interval: '3 дня 2 часа 11 минут',
		responsible: 'Соколов Арсений',
		group: 'группа выпускающего',
	}

	return (
		<Box
			sx={{
				height: '100%',
				width: '39%',
				backgroundColor: 'white',
				borderRadius: '8px',
				p: 2,
			}}
		></Box>
	)
}

export default SelectedStage
