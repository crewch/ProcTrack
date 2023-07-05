import { Box, Typography } from '@mui/material'
import { FC } from 'react'
import styles from '/src/styles/MainPageStyles/SelectedProcessStyles/InfoProcessStyles/DateInfoStyles/DateInfo.module.scss'
import { IDataInfoProps } from '../../../../../../interfaces/IMainPage/ISelectedProcess/IInfoProcess/ITextTags/IDateInfo/IDateInfo'

const DateInfo: FC<IDataInfoProps> = ({ startDate, endData, success }) => {
	return (
		<>
			<Box className={styles.container}>
				<Typography className={styles.typography1}>
					Время начала проверки
				</Typography>
				<Typography className={styles.typography2}>{startDate}</Typography>
			</Box>
			<Box className={styles.container}>
				<Typography className={styles.typography1}>
					Время окончания проверки
				</Typography>
				<Typography className={styles.typography2}>{endData}</Typography>
			</Box>
			<Box className={styles.container}>
				<Typography className={styles.typography1}>
					Время согласования
				</Typography>
				<Typography className={styles.typography2}>{success}</Typography>
			</Box>
		</>
	)
}

export default DateInfo
