import { useQueryClient, useMutation, useQueries } from '@tanstack/react-query'
import { FC, useState } from 'react'
import {
	Box,
	Button,
	Dialog,
	DialogTitle,
	DialogContent,
	List,
	ListItem,
	Checkbox,
	ListItemText,
	Typography,
	LinearProgress,
	Divider,
} from '@mui/material'
import { Stage } from '../../../../shared/interfaces/stage'
import { stageService } from '../../../../services/stage'
import { useGetUserData } from '../../../../hooks/userDataHook'

interface ButtonsProps {
	selectedStage: Stage
	isLoading: boolean
	isSuccess: boolean
}

const Buttons: FC<ButtonsProps> = ({ selectedStage, isSuccess, isLoading }) => {
	const queryClient = useQueryClient()

	const userId = useGetUserData().id

	const mutationSuccessStage = useMutation({
		mutationFn: () => stageService.successStage(selectedStage?.id, userId),
		onSuccess: () => {
			queryClient.invalidateQueries({
				queryKey: ['stageId'],
			})
			queryClient.invalidateQueries({
				queryKey: ['stagesAllByUserId'],
			})
			queryClient.invalidateQueries({
				queryKey: ['processByStageId'],
			})
		},
	})

	const mutationCancelStage = useMutation({
		mutationFn: () => stageService.cancelStage(selectedStage?.id, userId),
		onSuccess: () => {
			queryClient.invalidateQueries({
				queryKey: ['stageId'],
			})
			queryClient.invalidateQueries({
				queryKey: ['stagesAllByUserId'],
			})
			queryClient.invalidateQueries({
				queryKey: ['processByStageId'],
			})
		},
	})

	const userData = useGetUserData()

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

	const stages = useQueries({
		queries: selectedStage.canCreate.map(item => {
			return {
				queryKey: ['stageHeader', item],
				queryFn: () => stageService.getStageId(item),
			}
		}),
	})

	const mutationGetStages = useMutation({
		mutationFn: stageService.toggleStagePass,
		// onSuccess: () => {
		// 	queryClient.invalidateQueries({
		// 		queryKey: ['stageHeader'],
		// 	})
		// },
	})

	return (
		<>
			{isLoading && <LinearProgress />}
			{isSuccess && isBoss && (
				<>
					<Divider sx={{ my: '8px', borderWidth: '1px' }} />
					<Box sx={{ display: 'flex', gap: 2 }}>
						{selectedStage.status === 'Согласовано' ||
						selectedStage.status === 'Согласовано-Блокировано' ? (
							<Button
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
													.sort(
														(a, b) =>
															Number(b.data?.mark) - Number(a.data?.mark)
													)
													.map((item, index) => (
														<ListItem key={index}>
															{item.isSuccess && (
																<>
																	<Checkbox
																		onClick={() => {
																			//TODO:надо тестировать
																			const newData = { ...item }
																			if (newData.data?.pass !== undefined) {
																				newData.data.pass = !item.data?.pass
																			}

																			queryClient.setQueryData(
																				['stageHeader', item],
																				{
																					...item,
																					data: newData,
																				}
																			)

																			mutationGetStages.mutate({
																				stage: item.data,
																				userId,
																			})
																		}}
																		checked={!item.data?.pass}
																	/>
																	<ListItemText>
																		<Typography
																			sx={{
																				fontWeight: item.data?.mark ? 600 : 300,
																			}}
																		>
																			{item.data?.title}
																		</Typography>
																	</ListItemText>
																</>
															)}
														</ListItem>
													))}
											</List>
										</DialogContent>
									</Dialog>
								</>
							)}
					</Box>
				</>
			)}
		</>
	)
}

export default Buttons
