import { Box, LinearProgress } from '@mui/material'
import styles from '/src/styles/StageForSuccessPageStyles/SelectedProcessStyles/HeaderStyles/Header.module.scss'
import HeaderField from '../../../MainPage/SelectedProcess/InfoProcess/HeaderProcessField/HeaderField'
import UserField from '../../../MainPage/SelectedProcess/InfoProcess/UserField/UserField'
import FilesField from '../../../MainPage/SelectedProcess/InfoProcess/FilesField/FilesField'
import { FC } from 'react'
import { ISelectedProcessChildProps } from '../../../../interfaces/IStageForSuccessPage/ISelectedProcess/ISelectedProcess'

const Header: FC<ISelectedProcessChildProps> = ({
	selectedProcess,
	isLoading,
	isSuccess,
}) => {
	return (
		<Box className={styles.container}>
			{isLoading && <LinearProgress />}
			{isSuccess && selectedProcess && (
				<>
					<Box>
						<HeaderField
							name={selectedProcess.title}
							status={selectedProcess.status}
							importance={selectedProcess.priority}
							type={selectedProcess.type}
						/>
					</Box>
					<Box>
						<UserField
							responsible={selectedProcess.hold[0].users[0].longName}
							group={selectedProcess.hold[1].groups[0].title}
							role='Ответственный'
						/>
					</Box>
					<FilesField processId={selectedProcess.id} />
				</>
			)}
		</Box>
	)
}

export default Header
