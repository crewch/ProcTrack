import { Box, Button, LinearProgress } from '@mui/material'
import HeaderField from '../../../MainPage/SelectedStage/HeaderField/HeaderField'
import styles from '/src/styles/StageForSuccessPageStyles/SelectedStageStyles/HeaderStyles/Header.module.scss'
import UserField from '../../../MainPage/SelectedProcess/InfoProcess/UserField/UserField'
import { FC } from 'react'
import { ISelectedStageChildProps } from '../../../../interfaces/IStageForSuccessPage/ISelectedStage/ISelectedStage'
import { useMutation, useQueryClient } from '@tanstack/react-query'
import { stageApi } from '../../../../api/stageApi'

const Header: FC<ISelectedStageChildProps> = ({
	selectedStage,
	isLoading,
	isSuccess,
}) => {
	const queryClient = useQueryClient()

	const mutationSuccessStage = useMutation({
		mutationFn: () => stageApi.successStage(selectedStage?.id),
		onSuccess: () => {
			queryClient.invalidateQueries({
				queryKey: ['selectedStageStageForSuccessPage'],
			})
			queryClient.invalidateQueries({
				queryKey: ['stagesStageForSuccess'],
			})
			queryClient.invalidateQueries({
				queryKey: ['processByStageIdStageForSuccess'],
			})
		},
	})

	const mutationCancelStage = useMutation({
		mutationFn: () => stageApi.cancelStage(selectedStage?.id),
		onSuccess: () => {
			queryClient.invalidateQueries({
				queryKey: ['selectedStageStageForSuccessPage'],
			})
			queryClient.invalidateQueries({
				queryKey: ['stagesStageForSuccess'],
			})
			queryClient.invalidateQueries({
				queryKey: ['processByStageIdStageForSuccess'],
			})
		},
	})

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
					<Box className={styles.btns}>
						{selectedStage.status === 'Согласовано' ||
						selectedStage.status === 'Согласовано-Блокировано' ? (
							<Button
								className={styles.btn}
								size='small'
								color='error'
								variant='outlined'
								onClick={() => mutationCancelStage.mutate()}
							>
								Отменить Согласование
							</Button>
						) : (
							<Button
								color='success'
								className={styles.btn}
								size='small'
								variant='outlined'
								onClick={() => mutationSuccessStage.mutate()}
							>
								Согласовать
							</Button>
						)}
						<Button className={styles.btn} size='small' variant='outlined'>
							Редактировать путь согласования
						</Button>
					</Box>
				</>
			)}
		</Box>
	)
}

export default Header
