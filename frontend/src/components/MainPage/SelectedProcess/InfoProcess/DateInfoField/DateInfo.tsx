import { Box, Typography } from '@mui/material'
import { FC } from 'react'
import { IDataInfoProps } from '../../../../../interfaces/IMainPage/ISelectedProcess/IInfoProcess/ITextTags/IDateInfo/IDateInfo'

const DateInfo: FC<IDataInfoProps> = ({ startDate, endData, interval }) => {
	return (
		<>
			<Box sx={{ display: 'flex', pb: 1 }}>
				<Typography
					sx={{
						width: '40%',
						fontSize: '20px',
						fontFamily: 'Montserrat',
						fontWeight: 600,
						color: '#333333',
					}}
				>
					Дата начала
				</Typography>
				<Typography
					sx={{
						fontSize: '14px',
						color: '#333333',
						display: 'flex',
						alignItems: 'center',
					}}
				>
					{startDate}
				</Typography>
			</Box>
			<Box sx={{ display: 'flex', pb: 1 }}>
				<Typography
					sx={{
						width: '40%',
						fontSize: '20px',
						fontFamily: 'Montserrat',
						fontWeight: 600,
						color: '#333333',
					}}
				>
					Дата окончания
				</Typography>
				<Typography
					sx={{
						fontSize: '14px',
						color: '#333333',
						display: 'flex',
						alignItems: 'center',
					}}
				>
					{endData}
				</Typography>
			</Box>
			<Box sx={{ display: 'flex', pb: 1 }}>
				<Typography
					sx={{
						width: '40%',
						fontSize: '20px',
						fontFamily: 'Montserrat',
						fontWeight: 600,
						color: '#333333',
					}}
				>
					Осталось
				</Typography>
				<Typography
					sx={{
						fontSize: '14px',
						color: '#333333',
						display: 'flex',
						alignItems: 'center',
					}}
				>
					{interval}
				</Typography>
			</Box>
		</>
	)
}

export default DateInfo
