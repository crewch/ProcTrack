import { Box, Divider, Typography } from '@mui/material'
import { IInfoProcess } from '../../../../interfaces/IMainPage/ISelectedProcess/IInfoProcess/IInfoProcess'
import Pen from '/src/assets/pen.svg'
import User from '/src/assets/user1.svg'
import TextTegs from './TextTags/TextTags'
import DateInfo from './DateInfo/DateInfo'

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
	}

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
			<Box
				sx={{
					display: 'flex',
					flexDirection: 'column',
				}}
			>
				<Box
					sx={{ display: 'flex', justifyContent: 'space-between', mb: '2px' }}
				>
					<Typography
						variant='h4'
						sx={{
							fontFamily: 'Montserrat',
							fontWeight: 600,
							fontSize: '32px',
							color: '#1A2D67',
						}}
					>
						{process.name}
					</Typography>
					<Box
						sx={{
							display: 'flex',
							justifyContent: 'end',
							alignItems: 'center',
						}}
					>
						<img src={Pen} />
					</Box>
				</Box>
			</Box>
			<TextTegs
				importance={process.importance}
				status={process.status}
				type={process.type}
			/>
			<Divider sx={{ my: 1, borderWidth: 1 }} />
			<DateInfo
				startDate={process.startDate}
				endData={'ср, 27 декабря 2023 12:00'}
				interval={process.interval}
			/>
			<Divider sx={{ mb: 1, borderWidth: 1 }} />
			<Box sx={{ display: 'flex', gap: 1 }}>
				<Box
					sx={{
						display: 'flex',
						justifyContent: 'center',
						alignItems: 'center',
					}}
				>
					<img height='38px' src={User} />
				</Box>
				<Box sx={{ display: 'flex', flexDirection: 'column' }}>
					<Typography
						variant='h6'
						sx={{
							fontFamily: 'Montserrat',
							fontWeight: '600',
							fontSize: '20px',
							color: '#333333',
						}}
					>
						Ответственный
					</Typography>
					<Box sx={{ display: 'flex', gap: 1 }}>
						<Typography
							variant='body1'
							sx={{
								fontSize: '14px',
								color: '#333333',
							}}
						>
							{process.responsible}
						</Typography>
						<Box
							sx={{
								display: 'flex',
								alignItems: 'center',
								backgroundColor: '#ECECEC',
								px: '2px',
								borderRadius: '3px',
							}}
						>
							<Typography
								variant='body2'
								sx={{
									fontSize: '12px',
									color: '#333333',
								}}
							>
								{process.group}
							</Typography>
						</Box>
					</Box>
				</Box>
			</Box>
			<Divider sx={{ my: 1, borderWidth: 1 }} />
			<Box
				sx={{
					width: '270px',
					height: '60px',
					borderRadius: '9px',
					backgroundColor: '#ECECEC',
				}}
			>
				{/* TODO: Доделать */}
			</Box>
		</Box>
	)
}

export default InfoProcess
