import {
	Box,
	Button,
	Dialog,
	DialogContent,
	DialogTitle,
	LinearProgress,
} from '@mui/material'
import HeaderField from '../../../MainPage/SelectedStage/HeaderField/HeaderField'
import styles from '/src/styles/StageForSuccessPageStyles/SelectedStageStyles/HeaderStyles/Header.module.scss'
import UserField from '../../../MainPage/SelectedProcess/InfoProcess/UserField/UserField'
import { FC, useState } from 'react'
import { ISelectedStageChildProps } from '../../../../interfaces/IStageForSuccessPage/ISelectedStage/ISelectedStage'
import { useMutation, useQueryClient } from '@tanstack/react-query'
import { stageApi } from '../../../../api/stageApi'
import { IUser } from '../../../../interfaces/IApi/IApi'
import { IStage } from '../../../../interfaces/IApi/IGetStageApi'
import { getStageApi } from '../../../../api/getStageApi'

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

	const userDataText = localStorage.getItem('UserData')
	const userData: IUser = JSON.parse(userDataText ? userDataText : '')

	const [open, setOpen] = useState(false)

	const handleClickOpen = () => {
		setOpen(true)
	}

	const handleClose = () => {
		setOpen(false)
	}

	const [stages, setStages] = useState<IStage[]>([])

	const getStages = () => {
		selectedStage &&
			selectedStage.canCreate.forEach(async item => {
				const stage: IStage | null | undefined = await getStageApi.getStageId(
					item
				)

				if (stage) {
					setStages([...stages, stage])
				}
			})
	}

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
					{selectedStage.holds[0].groups[0].boss.id === userData.id ||
						(selectedStage.holds[1].groups[0].boss.id === userData.id && (
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
								{selectedStage.canCreate.length && (
									<>
										<Button
											className={styles.btn}
											size='small'
											variant='outlined'
											onClick={handleClickOpen}
										>
											Редактировать путь согласования
										</Button>
										<Dialog open={open} onClose={handleClose}>
											<DialogTitle>Редактировать путь согласования</DialogTitle>
											<DialogContent></DialogContent>
										</Dialog>
									</>
								)}
							</Box>
						))}
				</>
			)}
		</Box>
	)
}

export default Header
