import { Box, Divider, Skeleton } from '@mui/material'
import { useEffect, useState } from 'react'
import { useQuery } from '@tanstack/react-query'
import dayjs from 'dayjs'
import ToggleProcessButton from './ToggleProcessButton/ToggleProcessButton'
import UploadButtonDialog from './UploadButton/UploadButtonDialog'
import DateInfo from '@/components/shared/DateInfo/DateInfo'
import FilesField from '@/components/shared/FilesField/FilesField'
import InfoField from '@/components/shared/InfoField/InfoField'
import UserInfo from '@/components/shared/UserInfo/UserInfo'
import { useAppSelector } from '@/hooks/reduxHooks'
import { processService } from '@/services/process'
import styles from './InfoProcessRelease.module.scss'

const InfoProcessRelease = () => {
	const openedProcessID = useAppSelector(
		state => state.processStage.openedProcess
	)

	const {
		data: process,
		isSuccess,
		isLoading,
	} = useQuery({
		queryKey: ['processId', openedProcessID],
		queryFn: () => processService.getProcessId(openedProcessID),
	})

	const [intervalDate, setIntervalDate] = useState('')

	useEffect(() => {
		if (isSuccess && process && process.status === 'в процессе') {
			const interval = setInterval(() => {
				if (dayjs().isAfter(process.completedAtUnparsed)) {
					setIntervalDate('Время вышло')
					return () => clearInterval(interval)
				}

				setIntervalDate(
					`${dayjs(process.completedAtUnparsed).diff(dayjs(), 'day')}:${dayjs(
						process.completedAtUnparsed
					)
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
			{isLoading && <Skeleton variant='rounded' height='100%' />}
			{isSuccess && process && (
				<>
					<InfoField
						name={process.title}
						status={process.status}
						importance={process.priority}
						type={process.type}
						page='release'
						description={process.description}
					/>
					<Divider className={styles.divider} />
					<DateInfo
						startDate={process.createdAt}
						endData={process.completedAt}
						interval={intervalDate}
					/>
					<Divider className={styles.divider} />
					<UserInfo
						responsible={process.hold[0].users[0].longName}
						group={process.hold[1].groups[0].title}
						role='Ответственный'
					/>
					<Divider className={styles.divider} />
					<FilesField processId={process.id} />
					<Divider className={styles.divider} />
					<Box className={styles.btns}>
						<ToggleProcessButton status={process.status} />
						<UploadButtonDialog processId={process.id} />
					</Box>
				</>
			)}
		</Box>
	)
}

export default InfoProcessRelease
