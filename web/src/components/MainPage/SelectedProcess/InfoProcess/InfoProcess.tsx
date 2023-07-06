import { Box, Divider, LinearProgress, Typography } from '@mui/material'
import DateInfo from './DateInfoField/DateInfo'
import { ChangeEvent, useState } from 'react'
import UploadButton from './UploadButton/UploadButton'
import UserField from './UserField/UserField'
import HeaderField from './HeaderProcessField/HeaderField'
import FilesField from './FilesField/FilesField'
import styles from '/src/styles/MainPageStyles/SelectedProcessStyles/InfoProcessStyles/InfoProcess.module.scss'
import StopProcessButton from './StopProcessButton/StopProcessButton'
import { useQuery } from '@tanstack/react-query'
import { getProcessApi } from '../../../../api/getProcessApi'
import { useAppSelector } from '../../../../hooks/reduxHooks'
import StartProcessButton from './StartProcessButton/StartProcessButton'

const InfoProcess = () => {
	const openedProcessID = useAppSelector(state => state.processes.openedProcess)

	const { data, isSuccess, isLoading, isError, error } = useQuery({
		queryKey: ['processId', openedProcessID],
		queryFn: () => getProcessApi.getProcessId(openedProcessID),
	})

	const [_file, setFile] = useState<File>()
	const handleFileChange = (e: ChangeEvent<HTMLInputElement>) => {
		if (e.target.files) {
			setFile(e.target.files[0])
		}
	}

	return (
		<Box className={styles.container}>
			{isError && error instanceof Error && (
				<Typography variant='h4'>{error.message}</Typography>
			)}
			{isLoading && <LinearProgress />}
			{isSuccess && data && (
				<>
					<HeaderField
						name={data.title}
						status={data.status}
						importance={data.priority}
						type={data.type}
					/>
					<Divider className={styles.divider} />
					<DateInfo
						startDate={data.createdAt}
						endData={'ср, 27 декабря 2023 12:00'} //TODO:
						interval={data.expectedTime}
					/>
					<Divider className={styles.divider} />
					<UserField
						responsible={data.hold[0].users[0].longName}
						group={data.hold[1].groups[0].title}
						role='Ответственный'
					/>
					<Divider className={styles.divider} />
					<FilesField />
					<Divider className={styles.divider} />
					<Box className={styles.btns}>
						{data.status === 'в процессе' && <StopProcessButton />}
						{data.status === 'остановлен' && <StartProcessButton />}

						<UploadButton handleFileChange={handleFileChange} />
					</Box>
				</>
			)}
		</Box>
	)
}

export default InfoProcess
