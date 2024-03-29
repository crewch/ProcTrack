import { Box, Divider, Skeleton } from '@mui/material'
import { useQuery } from '@tanstack/react-query'
import dayjs from 'dayjs'
import { useState, useEffect } from 'react'
import DateInfo from '@/components/shared/DateInfo/DateInfo'
import FilesField from '@/components/shared/FilesField/FilesField'
import InfoField from '@/components/shared/InfoField/InfoField'
import UserInfo from '@/components/shared/UserInfo/UserInfo'
import { useAppSelector, useAppDispatch } from '@/hooks/reduxHooks'
import { processService } from '@/services/process'
import { setOpenedProcess } from '@/store/processStageSlice/processStageSlice'
import styles from './InfoProcessApproval.module.scss'

const InfoProcessApproval = () => {
	const selectedStage = useAppSelector(state => state.processStage.openedStage)
	const dispatch = useAppDispatch()

	const {
		data: selectedProcess,
		isLoading,
		isSuccess,
	} = useQuery({
		queryKey: ['processByStageId', selectedStage],
		queryFn: () => processService.getProcessByStageId(selectedStage),
	})

	const [intervalDate, setIntervalDate] = useState('')

	useEffect(() => {
		if (isSuccess && selectedProcess) {
			dispatch(setOpenedProcess({ id: selectedProcess.id }))
		}
	}, [selectedProcess, dispatch, isSuccess])

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
			{isLoading && <Skeleton variant='rounded' height='100%' />}
			{isSuccess && selectedProcess && (
				<>
					<InfoField
						name={selectedProcess.title}
						status={selectedProcess.status}
						importance={selectedProcess.priority}
						type={selectedProcess.type}
						page='approval'
						description={selectedProcess.description}
					/>
					<Divider className={styles.divider} />
					<DateInfo
						startDate={selectedProcess.createdAt}
						endData={selectedProcess.completedAt}
						interval={intervalDate}
					/>
					<Divider className={styles.divider} />
					<UserInfo
						responsible={selectedProcess.hold[0].users[0].longName}
						group={selectedProcess.hold[1].groups[0].title}
						role='Ответственный'
					/>
					<Divider className={styles.divider} />
					<FilesField processId={selectedProcess.id} />
				</>
			)}
		</Box>
	)
}

export default InfoProcessApproval
