import { Box, Divider, LinearProgress } from '@mui/material'
import { useQuery } from '@tanstack/react-query'
import { useAppDispatch, useAppSelector } from '../../../hooks/reduxHooks'
import FilesField from '../../MainPage/SelectedProcess/InfoProcess/FilesField/FilesField'
import UserField from '../../MainPage/SelectedProcess/InfoProcess/UserField/UserField'
import HeaderField from '../../MainPage/SelectedProcess/InfoProcess/HeaderProcessField/HeaderField'
import DateInfo from '../../MainPage/SelectedProcess/InfoProcess/DateInfoField/DateInfo'
import dayjs from 'dayjs'
import { useState, useEffect } from 'react'
import { setOpenedProcess } from '../../../store/processStageSlice/processStageSlice'
import { processService } from '../../../services/process'
import styles from '/src/styles/StageForSuccessPageStyles/SelectedProcessStyles/SelectedProcess.module.scss'

const SelectedProcess = () => {
	const selectedStage = useAppSelector(state => state.processStage.openedStage)
	const dispatch = useAppDispatch()

	const {
		data: selectedProcess,
		isLoading,
		isSuccess,
	} = useQuery({
		queryKey: ['processByStageIdStageForSuccess', selectedStage],
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
			{isLoading && <LinearProgress />}
			{isSuccess && selectedProcess && (
				<>
					<HeaderField
						name={selectedProcess.title}
						status={selectedProcess.status}
						importance={selectedProcess.priority}
						type={selectedProcess.type}
						page='stageForSuccess'
					/>
					<Divider className={styles.divider} />
					<DateInfo
						startDate={selectedProcess.createdAt}
						endData={selectedProcess.completedAt}
						interval={intervalDate}
					/>
					<Divider className={styles.divider} />
					<UserField
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

export default SelectedProcess
