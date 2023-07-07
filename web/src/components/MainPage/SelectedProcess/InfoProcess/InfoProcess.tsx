import { Box, Divider, LinearProgress } from '@mui/material'
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

	const {
		data: process,
		isSuccess,
		isLoading,
	} = useQuery({
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
			{isLoading && <LinearProgress />}
			{isSuccess && process && (
				<>
					<HeaderField
						name={process.title}
						status={process.status}
						importance={process.priority}
						type={process.type}
					/>
					<Divider className={styles.divider} />
					<DateInfo
						startDate={process.createdAt} //TODO:
						endData={'ср, 27 декабря 2023 12:00'} //TODO:
						interval={process.expectedTime} //TODO:
					/>
					<Divider className={styles.divider} />
					<UserField
						responsible={process.hold[0].users[0].longName} //TODO:
						group={process.hold[1].groups[0].title} //TODO:
						role='Ответственный'
					/>
					<Divider className={styles.divider} />
					<FilesField />
					<Divider className={styles.divider} />
					<Box className={styles.btns}>
						{process.status === 'в процессе' && <StopProcessButton />}
						{process.status === 'остановлен' && <StartProcessButton />}
						<UploadButton
							handleFileChange={handleFileChange} //TODO:
						/>
					</Box>
				</>
			)}
		</Box>
	)
}

export default InfoProcess
