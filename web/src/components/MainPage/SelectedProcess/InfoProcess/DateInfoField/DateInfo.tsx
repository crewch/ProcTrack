import { Box, Typography } from '@mui/material'
import { FC, memo } from 'react'
import { IDataInfoProps } from '../../../../../interfaces/IMainPage/ISelectedProcess/IInfoProcess/ITextTags/IDateInfo/IDateInfo'
import styles from '/src/styles/MainPageStyles/SelectedProcessStyles/InfoProcessStyles/DateInfoStyles/DateInfo.module.scss'

const DateInfo: FC<IDataInfoProps> = memo(
	({ startDate, endData, interval, success, confirm }) => {
		return (
			<>
				<Box className={styles.container}>
					<Typography className={styles.typography1}>Время начала</Typography>
					<Typography className={styles.typography2}>{startDate}</Typography>
				</Box>
				{endData && (
					<Box className={styles.container}>
						<Typography className={styles.typography1}>
							Время окончания
						</Typography>
						<Typography className={styles.typography2}>{endData}</Typography>
					</Box>
				)}
				{success && (
					<Box className={styles.container}>
						<Typography className={styles.typography1}>
							Время электронного согласования
						</Typography>
						<Typography className={styles.typography2}>{success}</Typography>
					</Box>
				)}
				{confirm && (
					<Box className={styles.container}>
						<Typography className={styles.typography1}>
							Время согласования
						</Typography>
						<Typography className={styles.typography2}>{confirm}</Typography>
					</Box>
				)}
				{interval && (
					<Box className={styles.container}>
						<Typography className={styles.typography1}>Осталось</Typography>
						<Typography className={styles.typography2}>{interval}</Typography>
					</Box>
				)}
			</>
		)
	}
)

export default DateInfo
