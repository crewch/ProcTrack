import { Box, Typography } from '@mui/material'
import { FC, memo } from 'react'
import styles from './DateInfo.module.scss'

interface DataInfoProps {
	startDate?: string
	endData?: string
	interval?: string
	success?: string
	confirm?: string
	startDateCheck?: string
	endDateCheck?: string
}

const DateInfo: FC<DataInfoProps> = memo(
	({
		startDate,
		endData,
		startDateCheck,
		endDateCheck,
		interval,
		success,
		confirm,
	}) => {
		return (
			<Box>
				{startDate && (
					<Box className={styles.container}>
						<Typography className={styles.typography1}>Время начала</Typography>
						<Typography className={styles.typography2}>{startDate}</Typography>
					</Box>
				)}
				{endData && (
					<Box className={styles.container}>
						<Typography className={styles.typography1}>
							Время окончания
						</Typography>
						<Typography className={styles.typography2}>{endData}</Typography>
					</Box>
				)}
				{startDateCheck && (
					<Box className={styles.container}>
						<Typography className={styles.typography1}>
							Время начала проверки
						</Typography>
						<Typography className={styles.typography2}>
							{startDateCheck}
						</Typography>
					</Box>
				)}
				{endDateCheck && (
					<Box className={styles.container}>
						<Typography className={styles.typography1}>
							Время окончания проверки
						</Typography>
						<Typography className={styles.typography2}>
							{endDateCheck}
						</Typography>
					</Box>
				)}
				{confirm && (
					<Box className={styles.container}>
						<Typography className={styles.typography1}>
							Время электронного согласования
						</Typography>
						<Typography className={styles.typography2}>{confirm}</Typography>
					</Box>
				)}
				{success && (
					<Box className={styles.container}>
						<Typography className={styles.typography1}>
							Время согласования
						</Typography>
						<Typography className={styles.typography2}>{success}</Typography>
					</Box>
				)}
				{interval && (
					<Box className={styles.container}>
						<Typography className={styles.typography1}>Осталось</Typography>
						<Typography className={styles.typography2}>{interval}</Typography>
					</Box>
				)}
			</Box>
		)
	}
)

export default DateInfo
