import { Box, LinearProgress } from '@mui/material'
import DateInfo from '../../../MainPage/SelectedProcess/InfoProcess/DateInfoField/DateInfo'
import styles from '/src/styles/StageForSuccessPageStyles/SelectedProcessStyles/FooterStyles/Footer.module.scss'
import { FC, useEffect, useState } from 'react'
import { ISelectedProcessChildProps } from '../../../../interfaces/IStageForSuccessPage/ISelectedProcess/ISelectedProcess'
import dayjs from 'dayjs'

const Footer: FC<ISelectedProcessChildProps> = ({
	selectedProcess,
	isLoading,
	isSuccess,
}) => {
	const [intervalDate, setIntervalDate] = useState('')

	useEffect(() => {
		if (
			isSuccess &&
			selectedProcess &&
			selectedProcess.status === 'в процессе'
		) {
			const interval = setInterval(() => {
				if (dayjs().isAfter(selectedProcess.completedAtUnparsed)) {
					setIntervalDate('Время вышло')
					return () => clearInterval(interval)
				}

				setIntervalDate(
					`${dayjs(selectedProcess.completedAtUnparsed).diff(
						dayjs(),
						'day'
					)}:${dayjs(selectedProcess.completedAtUnparsed)
						.subtract(dayjs().hour() + 1, 'hour')
						.hour()}:${dayjs(selectedProcess.completedAtUnparsed)
						.subtract(dayjs().minute(), 'minute')
						.minute()}:${dayjs(selectedProcess.completedAtUnparsed)
						.subtract(dayjs().second(), 'second')
						.second()}`
				)
			}, 1000)

			return () => clearInterval(interval)
		}
	}, [isSuccess, selectedProcess])

	return (
		<Box className={styles.container}>
			{isLoading && <LinearProgress />}
			{isSuccess && selectedProcess && (
				<DateInfo
					page='stageForSuccess'
					startDate={selectedProcess.createdAt}
					endData={selectedProcess.completedAt}
					interval={intervalDate}
				/>
			)}
		</Box>
	)
}

export default Footer
