import { Box, Typography } from '@mui/material'
import { FC } from 'react'
import { IDataInfoProps } from '../../../../../interfaces/IMainPage/ISelectedProcess/IInfoProcess/ITextTags/IDateInfo/IDateInfo'
import styles from '/src/styles/MainPageStyles/SelectedProcessStyles/InfoProcessStyles/DateInfoStyles/DateInfo.module.scss'

const DateInfo: FC<IDataInfoProps> = ({ startDate, endData, interval }) => {
	return (
		<>
			<Box className={styles.container}>
				<Typography className={styles.typography1}>Дата начала</Typography>
				<Typography className={styles.typography2}>{startDate}</Typography>
			</Box>
			<Box className={styles.container}>
				<Typography className={styles.typography1}>Дата окончания</Typography>
				<Typography className={styles.typography2}>{endData}</Typography>
			</Box>
			{interval && (
				<Box className={styles.container}>
					<Typography className={styles.typography1}>Осталось</Typography>
					<Typography className={styles.typography2}>{interval}</Typography>
				</Box>
			)}
		</>
	)
}

export default DateInfo
