import { Box, Button, LinearProgress } from '@mui/material'
import HeaderField from '../../../MainPage/SelectedStage/HeaderField/HeaderField'
import styles from '/src/styles/StageForSuccessPageStyles/SelectedStageStyles/HeaderStyles/Header.module.scss'
import UserField from '../../../MainPage/SelectedProcess/InfoProcess/UserField/UserField'
import { FC } from 'react'
import { ISelectedStageChildProps } from '../../../../interfaces/IStageForSuccessPage/ISelectedStage/ISelectedStage'

const Header: FC<ISelectedStageChildProps> = ({
	selectedStage,
	isLoading,
	isSuccess,
}) => {
	return (
		<Box className={styles.container}>
			{isLoading && <LinearProgress />}
			{isSuccess && selectedStage && (
				<>
					<Box className={styles.headerField}>
						<HeaderField
							name={selectedStage.title}
							status={selectedStage.status}
							nameOfGroup={
								selectedStage?.holds[0]?.groups[0]?.title ||
								selectedStage?.holds[1]?.groups[0]?.title
							}
						/>
					</Box>
					<Box>
						<UserField
							group={
								selectedStage?.holds[0]?.groups[0]?.title ||
								selectedStage?.holds[1]?.groups[0]?.title
							}
							responsible={
								selectedStage?.holds[0]?.groups[0]?.boss.longName ||
								selectedStage?.holds[1]?.groups[0]?.boss.longName
							}
							role={'Главный согласующий'}
						/>
					</Box>
					<Button color='error' variant='outlined'>
						Отменить утверждение
					</Button>
				</>
			)}
		</Box>
	)
}

export default Header
