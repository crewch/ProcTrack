import { Box, Divider, LinearProgress } from '@mui/material'
import { useEffect, useState } from 'react'
import { useQuery } from '@tanstack/react-query'
import { getProcessApi } from '../../../../api/getProcessApi'
import { useAppSelector } from '../../../../hooks/reduxHooks'
import DateInfo from './DateInfoField/DateInfo'
import UploadButton from './UploadButton/UploadButton'
import UserField from './UserField/UserField'
import HeaderField from './HeaderProcessField/HeaderField'
import FilesField from './FilesField/FilesField'
import styles from '/src/styles/MainPageStyles/SelectedProcessStyles/InfoProcessStyles/InfoProcess.module.scss'
import StopProcessButton from './StopProcessButton/StopProcessButton'
import StartProcessButton from './StartProcessButton/StartProcessButton'
import dayjs from 'dayjs'

const InfoProcess = () => {
	const openedProcessID = useAppSelector(state => state.processes.openedProcess)

	const {
		data: process,
		isSuccess,
		isLoading,
	} = useQuery({
		queryKey: ['processId', openedProcessID],
		queryFn: () => getProcessApi.getProcessId(openedProcessID),
	})

	const [intervalDate, setIntervalDate] = useState(' ')

	useEffect(() => {
		if (isSuccess && process && process.status === 'в процессе') {
			const interval = setInterval(() => {
				if (dayjs().isAfter(process.completedAtUnparsed)) {
					setIntervalDate('Время вышло')
					return () => clearInterval(interval)
				}

				setIntervalDate(
					`${dayjs(process.completedAtUnparsed)
						.subtract(dayjs().day(), 'day')
						.day()}:${dayjs(process.completedAtUnparsed)
						.subtract(dayjs().hour() + 1, 'hour')
						.hour()}:${dayjs(process.completedAtUnparsed)
						.subtract(dayjs().minute(), 'minute')
						.minute()}:${dayjs(process.completedAtUnparsed)
						.subtract(dayjs().second(), 'second')
						.second()}`
				)
			}, 1000)

			return () => clearInterval(interval)
		}
	}, [isSuccess, process])

	return (
		<Box className={styles.container}>
			{isLoading && <LinearProgress />}
			{isSuccess && process && (
				<>
					<HeaderField
						name={process.title}
						status={process.status}
						importance={process.priority}
						type={process.type}
						page='main'
					/>
					<Divider className={styles.divider} />
					<DateInfo
						startDate={process.createdAt}
						endData={process.completedAt}
						interval={intervalDate}
						page='main'
					/>
					<Divider className={styles.divider} />
					<UserField
						responsible={process.hold[0].users[0].longName}
						group={process.hold[1].groups[0].title}
						role='Ответственный'
					/>
					<Divider className={styles.divider} />
					<FilesField processId={process.id} />
					<Divider className={styles.divider} />
					<Box className={styles.btns}>
						{process.status === 'в процессе' && <StopProcessButton />}
						{process.status === 'остановлен' && <StartProcessButton />}
						<UploadButton processId={process.id} />
					</Box>
				</>
			)}
		</Box>
	)
}

export default InfoProcess
