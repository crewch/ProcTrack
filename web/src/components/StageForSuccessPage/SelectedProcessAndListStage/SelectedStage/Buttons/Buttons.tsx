import { useQueryClient, useMutation, useQueries } from '@tanstack/react-query'
import { FC, useState } from 'react'
import { getStageApi } from '../../../../../api/getStageApi'
import { stageApi } from '../../../../../api/stageApi'
import { IUser } from '../../../../../interfaces/IApi/IApi'
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
} from '@mui/material'
import { ISelectedStageChildProps } from '../../../../../interfaces/IStageForSuccessPage/ISelectedStage/ISelectedStage'

const Buttons: FC<ISelectedStageChildProps> = ({
	selectedStage,
	isSuccess,
	isLoading,
}) => {
	const queryClient = useQueryClient()

	const mutationSuccessStage = useMutation({
		mutationFn: () => stageApi.successStage(selectedStage?.id),
		onSuccess: () => {
			queryClient.invalidateQueries({
				queryKey: ['stageId'],
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
				queryKey: ['stageId'],
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

	const stages = useQueries({
		queries: selectedStage.canCreate.map(item => {
			return {
				queryKey: ['stageHeader', item],
				queryFn: () => getStageApi.getStageId(item),
			}
		}),
	})

	const mutationGetStages = useMutation({
		mutationFn: stageApi.toggleStagePass,
		onSuccess: () => {
			queryClient.invalidateQueries({
				queryKey: ['stageHeader'],
			})
		},
	})

	return (
		<>
			{isLoading && <LinearProgress />}
			{isSuccess && isBoss && (
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
													(a, b) => Number(b.data?.mark) - Number(a.data?.mark)
												)
												.map((item, index) => (
													<ListItem key={index}>
														{item.isSuccess && (
															<>
																<Checkbox
																	onClick={() => {
																		mutationGetStages.mutate(item.data)
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
			)}
		</>
	)
}

export default Buttons
