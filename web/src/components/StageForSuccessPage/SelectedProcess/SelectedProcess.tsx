import { Box, Divider, LinearProgress } from '@mui/material'
import { useQuery } from '@tanstack/react-query'
import { useAppDispatch, useAppSelector } from '../../../hooks/reduxHooks'
import { getProcessApi } from '../../../api/getProcessApi'
import FilesField from '../../MainPage/SelectedProcess/InfoProcess/FilesField/FilesField'
import UserField from '../../MainPage/SelectedProcess/InfoProcess/UserField/UserField'
import HeaderField from '../../MainPage/SelectedProcess/InfoProcess/HeaderProcessField/HeaderField'
import DateInfo from '../../MainPage/SelectedProcess/InfoProcess/DateInfoField/DateInfo'
import dayjs from 'dayjs'
import { useState, useEffect } from 'react'
import { setOpenedProcess } from '../../../store/processSlice/processSlice'
import styles from '/src/styles/StageForSuccessPageStyles/SelectedProcessStyles/SelectedProcess.module.scss'

const SelectedProcess = () => {
	const selectedStage = useAppSelector(state => state.processes.openedStage)
	const dispatch = useAppDispatch()

	const { data, isLoading, isSuccess } = useQuery({
		queryKey: ['processByStageIdStageForSuccess', selectedStage],
		queryFn: () => getProcessApi.getProcessByStageId(selectedStage),
	})

	const [intervalDate, setIntervalDate] = useState('')

	useEffect(() => {
		if (isSuccess && data) {
			dispatch(setOpenedProcess({ id: data.id }))
		}
	}, [data, dispatch, isSuccess])

	useEffect(() => {
		if (isSuccess && data && data.status === 'в процессе') {
			const interval = setInterval(() => {
				if (dayjs().isAfter(data.completedAtUnparsed)) {
					setIntervalDate('Время вышло')
					return () => clearInterval(interval)
				}

				setIntervalDate(
					`${dayjs(data.completedAtUnparsed).diff(dayjs(), 'day')}:${dayjs(
						data.completedAtUnparsed
					)
						.subtract(dayjs().hour() + 1, 'hour')
						.hour()}:${dayjs(data.completedAtUnparsed)
						.subtract(dayjs().minute(), 'minute')
						.minute()}:${dayjs(data.completedAtUnparsed)
						.subtract(dayjs().second(), 'second')
						.second()}`
				)
			}, 1000)

			return () => clearInterval(interval)
		}
	}, [isSuccess, data])

	return (
		<Box className={styles.container}>
			{isLoading && <LinearProgress />}
			{isSuccess && data && (
				<>
					<HeaderField
						name={data.title}
						status={data.status}
						importance={data.priority}
						type={data.type}
						page='main'
					/>
					<Divider className={styles.divider} />
					<DateInfo
						startDate={data.createdAt}
						endData={data.completedAt}
						interval={intervalDate}
					/>
					<Divider className={styles.divider} />
					<UserField
						responsible={data.hold[0].users[0].longName}
						group={data.hold[1].groups[0].title}
						role='Ответственный'
					/>
					<Divider className={styles.divider} />
					<FilesField processId={data.id} />
				</>
			)}
		</Box>
	)
}

export default SelectedProcess
