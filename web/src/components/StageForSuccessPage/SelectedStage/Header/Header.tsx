import {
	Box,
	Button,
	Checkbox,
	Dialog,
	DialogContent,
	DialogTitle,
	LinearProgress,
	List,
	ListItem,
	ListItemText,
	Typography,
} from '@mui/material'
import HeaderField from '../../../MainPage/SelectedStage/HeaderField/HeaderField'
import styles from '/src/styles/StageForSuccessPageStyles/SelectedStageStyles/HeaderStyles/Header.module.scss'
import UserField from '../../../MainPage/SelectedProcess/InfoProcess/UserField/UserField'
import { FC, useEffect, useState } from 'react'
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

	const isBoss =
		selectedStage?.holds[0]?.groups[0]?.boss?.id === userData.id ||
		selectedStage?.holds[1]?.groups[0]?.boss?.id === userData.id

	const [stages, setStages] = useState<IStage[]>([])
	const [stageFlag, setStageFlag] = useState(false)

	useEffect(() => {
		setStages([])
	}, [isBoss, selectedStage, stageFlag])

	useEffect(() => {
		if (isBoss) {
			const getStages = async () => {
				if (selectedStage) {
					for (const item of selectedStage.canCreate) {
						const stage = await getStageApi.getStageId(item)

						if (stage) {
							setStages(prevStages => [...prevStages, stage])
						}
					}
				}
			}

			getStages()
		}
	}, [isBoss, selectedStage, stageFlag])

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
					{isBoss && (
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
							{selectedStage.status !== 'Согласовано' &&
								selectedStage.status !== 'Согласовано-Блокировано' &&
								selectedStage.canCreate.length > 0 && (
									<>
										<Button
											className={styles.btn}
											size='small'
											variant='outlined'
											onClick={() => {
												handleClickOpen()
											}}
										>
											Редактировать путь согласования
										</Button>
										<Dialog
											PaperProps={{
												sx: {
													width: '30%',
													height: '40%',
													borderRadius: '16px',
													p: 1,
												},
											}}
											open={open}
											onClose={handleClose}
										>
											<DialogTitle>Редактировать путь согласования</DialogTitle>
											<DialogContent>
												<List sx={{ height: '100%', overflow: 'auto' }}>
													{stages
														.sort((a, b) => Number(b.mark) - Number(a.mark))
														.map((item, index) => (
															<ListItem key={index}>
																<Checkbox
																	onClick={() => {
																		stageApi.toggleStagePass(item)
																		setStageFlag(!stageFlag)
																	}}
																	checked={!item.pass}
																/>
																<ListItemText>
																	<Typography
																		sx={{ fontWeight: item.mark ? 600 : 300 }}
																	>
																		{item.title}
																	</Typography>
																</ListItemText>
															</ListItem>
														))}
												</List>
											</DialogContent>
										</Dialog>
									</>
								)}
						</Box>
					)}
				</>
			)}
		</Box>
	)
}

export default Header
