import { Box, Typography } from '@mui/material'
import { FC } from 'react'
import styles from '/src/styles/MainPageStyles/SelectedProcessStyles/InfoProcessStyles/DateInfoStyles/DateInfo.module.scss'

interface DataInfoProps {
	startDate?: string
	endData?: string
	interval?: string
	success?: string
	confirm?: string
}

const DateInfo: FC<DataInfoProps> = ({ startDate, endData, success }) => {
	return (
		<Box>
			{startDate && (
				<Box className={styles.container}>
					<Typography className={styles.stageForSuccessPageTypography1}>
						Время начала проверки
					</Typography>
					<Typography className={styles.typography2}>{startDate}</Typography>
				</Box>
			)}
			{endData && (
				<Box className={styles.container}>
					<Typography className={styles.stageForSuccessPageTypography1}>
						Время окончания проверки
					</Typography>
					<Typography className={styles.typography2}>{endData}</Typography>
				</Box>
			)}
			{success && (
				<Box className={styles.container}>
					<Typography className={styles.stageForSuccessPageTypography1}>
						Время согласования
					</Typography>
					<Typography className={styles.typography2}>{success}</Typography>
				</Box>
			)}
		</Box>
	)
}

export default DateInfo
