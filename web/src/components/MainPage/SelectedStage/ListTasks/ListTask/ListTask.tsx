import { ChangeEvent, FC, memo, useState } from 'react'
import { IListTaskProps } from '../../../../../interfaces/IMainPage/ISelectedStage/IListTasks/IListTask/IListTaskProps'
import {
	Box,
	Button,
	Divider,
	Link,
	List,
	ListItem,
	ListItemIcon,
	ListItemText,
	TextField,
	Tooltip,
	Typography,
} from '@mui/material'
import DateInfo from './DataInfo/DataInfo'
import UserField from '../../../SelectedProcess/InfoProcess/UserField/UserField'
import { CustomButton } from '../../../../CustomButton/CustomButton'
import { useMutation, useQueryClient } from '@tanstack/react-query'
import { commentsApi } from '../../../../../api/commentsApi'
import { IComment } from '../../../../../interfaces/IApi/IGetTask'
import { fileApi } from '../../../../../api/fileApi'
import { switchTaskApi } from '../../../../../api/switchTaskApi'
import { IUser } from '../../../../../interfaces/IApi/IApi'
import styles from '/src/styles/MainPageStyles/SelectedStageStyles/ListTasksStyles/ListTaskStyles/ListTask.module.scss'

const ListTask: FC<IListTaskProps> = memo(
	({
		startDate,
		endDate,
		successDate,
		roleAuthor,
		author,
		group,
		remarks,
		taskId,
		page,
	}) => {
		const [textComment, setTextComment] = useState('')
		const [fileRef, setFileRef] = useState('')

		const handleFileChange = (e: ChangeEvent<HTMLInputElement>) => {
			if (e && e?.target && e.target?.files) {
				const formData = new FormData()
				formData.append('file', e.target.files[0])

				const getData = async () => {
					const data = await fileApi.sendFile(formData)

					setFileRef(data)
				}
				getData()

				e.target.value = ''
			}
		}

		const userDataText = localStorage.getItem('UserData')
		const userData: IUser = JSON.parse(userDataText ? userDataText : '')

		const comment: IComment = {
			id: 0,
			text: textComment,
			fileRef: fileRef,
			user: userData,
			createdAt: '',
		}

		const queryClient = useQueryClient()
		const mutationSendComment = useMutation({
			mutationFn: () => commentsApi.sendComments(taskId, comment),
			onSuccess: () => {
				setTextComment('')
				setFileRef('')
				queryClient.invalidateQueries({ queryKey: ['tasks'] })
			},
		})

		const mutationStartTask = useMutation({
			mutationFn: () => switchTaskApi.startTaskId(taskId, userData.id),
			onSuccess: () => {
				queryClient.invalidateQueries({ queryKey: ['stagesStageForSuccess'] })
				queryClient.invalidateQueries({
					queryKey: ['stageId'],
				})
				queryClient.invalidateQueries({
					queryKey: ['tasks'],
				})
			},
		})

		const mutationStopTask = useMutation({
			mutationFn: () => switchTaskApi.stopTaskId(taskId, userData.id),
			onSuccess: () => {
				queryClient.invalidateQueries({ queryKey: ['stagesStageForSuccess'] })
				queryClient.invalidateQueries({
					queryKey: ['stageId'],
				})
				queryClient.invalidateQueries({
					queryKey: ['tasks'],
				})
			},
		})

		const mutationEndVerificationTask = useMutation({
			mutationFn: () =>
				switchTaskApi.endVerificationTaskId(taskId, userData.id),
			onSuccess: () => {
				queryClient.invalidateQueries({ queryKey: ['stagesStageForSuccess'] })
				queryClient.invalidateQueries({
					queryKey: ['stageId'],
				})
				queryClient.invalidateQueries({
					queryKey: ['tasks'],
				})
			},
		})

		const mutationAssignsTask = useMutation({
			mutationFn: () => switchTaskApi.assignTaskId(taskId, userData.id),
			onSuccess: () => {
				queryClient.invalidateQueries({ queryKey: ['stagesStageForSuccess'] })
				queryClient.invalidateQueries({
					queryKey: ['stageId'],
				})
				queryClient.invalidateQueries({
					queryKey: ['tasks'],
				})
			},
		})

		return (
			<Box className={styles.container}>
				{page === 'stageForSuccess' && !startDate ? (
					<Button
						className={styles.startBtn}
						variant='outlined'
						onClick={() => mutationStartTask.mutate()}
					>
						начать согласование
					</Button>
				) : page === 'stageForSuccess' && !endDate ? (
					<Box className={styles.dateField}>
						<DateInfo
							startDate={startDate && startDate}
							endData={endDate && endDate}
							success={successDate && successDate}
						/>
						<Box className={styles.btns}>
							<Button
								className={styles.btn}
								variant='outlined'
								onClick={() => mutationEndVerificationTask.mutate()}
							>
								отметить конец проверки
							</Button>
							{startDate && (
								<Button
									color='error'
									variant='outlined'
									onClick={() => mutationStopTask.mutate()}
								>
									отменить
								</Button>
							)}
						</Box>
					</Box>
				) : (
					page === 'stageForSuccess' && (
						<Box className={styles.dateField}>
							<DateInfo
								startDate={startDate && startDate}
								endData={endDate && endDate}
								success={successDate && successDate}
							/>
							<Box className={styles.btns}>
								{!successDate && (
									<Button
										className={styles.btn}
										variant='outlined'
										onClick={() => mutationAssignsTask.mutate()}
									>
										согласовать задание
									</Button>
								)}
								{startDate && (
									<Button
										color='error'
										variant='outlined'
										onClick={() => mutationStopTask.mutate()}
									>
										отменить
									</Button>
								)}
							</Box>
						</Box>
					)
				)}
				<Divider className={styles.divider} />
				<UserField group={group} responsible={author} role={roleAuthor} />
				<Divider className={styles.divider} />
				<Typography className={styles.typography}>Замечания:</Typography>
				{!remarks.length && (
					<Typography className={styles.typographySmall}>
						Нет замечаний
					</Typography>
				)}
				{remarks.length > 0 && (
					<List className={styles.list}>
						{remarks.map((remark, index) => (
							<ListItem divider key={index}>
								<ListItemIcon>
									<img src='/user1.svg' className={styles.img} />
								</ListItemIcon>
								<ListItemText className={styles.reportText}>
									<Box className={styles.title}>
										<Typography>{remark.user.longName}</Typography>
										<Typography>{remark.createdAt}</Typography>
									</Box>
									<Box className={styles.text}>
										<Typography>{remark.text}</Typography>
										<Tooltip arrow title={remark.fileRef}>
											<Link
												onClick={() => fileApi.getFile(remark.fileRef)}
												component='button'
											>
												{remark.fileRef && remark.fileRef.slice(0, 20) + '...'}
											</Link>
										</Tooltip>
									</Box>
								</ListItemText>
							</ListItem>
						))}
					</List>
				)}
				<Box className={styles.addCommentContainer}>
					<Box className={styles.inputField}>
						<TextField
							value={textComment}
							onChange={event => setTextComment(event.target.value)}
							placeholder='Введите сообщение'
							autoComplete='off'
							variant='standard'
							InputProps={{
								className: styles.InputProps,
								disableUnderline: true,
							}}
							className={styles.TextField}
						/>
						<Box component='label'>
							<CustomButton
								className={`${styles.uploadBtn} ${
									fileRef ? styles.loadedBtn : ''
								}`}
								component='span'
								variant='contained'
							>
								<img src='/folderUpload.svg' height='20px' width='20px' />
							</CustomButton>
							<input hidden type='file' onChange={handleFileChange} />
						</Box>
					</Box>
					<Box className={styles.btns}>
						<CustomButton
							disabled={!textComment}
							onClick={() => {
								setTextComment('')
								setFileRef('')
							}}
						>
							Отмена
						</CustomButton>
						<CustomButton
							disabled={!textComment}
							onClick={() => mutationSendComment.mutate()}
						>
							Отправить
						</CustomButton>
					</Box>
				</Box>
			</Box>
		)
	}
)

export default ListTask
