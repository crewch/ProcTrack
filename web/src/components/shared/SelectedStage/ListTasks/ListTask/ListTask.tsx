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
import { ChangeEvent, FC, memo, useState } from 'react'
import { useMutation } from '@tanstack/react-query'
import { Comment } from '@/shared/interfaces/comment'
import { ReactComponent as User } from '@/assets/user1.svg'
import classNames from 'classnames'
import DateInfo from '@/components/shared/DateInfo/DateInfo'
import UserInfo from '@/components/shared/UserInfo/UserInfo'
import { GrayButton } from '@/components/ui/button/GrayButton'
import { commentService } from '@/services/comment'
import { fileService } from '@/services/file'
import { taskService } from '@/services/task'
import styles from './ListTask.module.scss'
import { getUserData } from '@/utils/getUserData'

export interface ListTaskProps {
	startDate: string
	endDate: string
	successDate: string
	roleAuthor: string
	author: string
	group: string
	remarks: Comment[]
	taskId: number
	page: 'release' | 'approval'
}

const ListTask: FC<ListTaskProps> = memo(
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
					const data = await fileService.sendFile(formData)

					setFileRef(data)
				}
				getData()

				e.target.value = ''
			}
		}

		const userData = getUserData()

		const comment: Comment = {
			id: 0,
			text: textComment,
			fileRef: fileRef,
			user: userData,
			createdAt: '',
		}

		const mutationSendComment = useMutation({
			mutationFn: () => commentService.sendComment(taskId, comment),
			onSuccess: () => {
				setTextComment('')
				setFileRef('')
			},
		})

		const mutationStartTask = useMutation({
			mutationFn: () => taskService.startTaskId(taskId, userData.id),
		})

		const mutationStopTask = useMutation({
			mutationFn: () => taskService.stopTaskId(taskId, userData.id),
		})

		const mutationEndVerificationTask = useMutation({
			mutationFn: () => taskService.endVerificationTaskId(taskId, userData.id),
		})

		const mutationAssignsTask = useMutation({
			mutationFn: () => taskService.assignTaskId(taskId, userData.id),
		})

		return (
			<Box className={styles.container}>
				{page === 'approval' && !startDate ? (
					<Button
						className={styles.startBtn}
						variant='outlined'
						onClick={() => mutationStartTask.mutate()}
					>
						начать согласование
					</Button>
				) : page === 'approval' && !endDate ? (
					<Box className={styles.dateField}>
						<DateInfo
							startDateCheck={startDate && startDate}
							endDateCheck={endDate && endDate}
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
					page === 'approval' && (
						<Box className={styles.dateField}>
							<DateInfo
								startDateCheck={startDate && startDate}
								endDateCheck={endDate && endDate}
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
				<UserInfo group={group} responsible={author} role={roleAuthor} />
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
									<User className={styles.img} />
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
												onClick={() => fileService.getFile(remark.fileRef)}
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
							<GrayButton
								className={classNames(styles.uploadBtn, {
									[styles.loadedBtn]: fileRef,
								})}
								component='span'
								variant='contained'
							>
								<img src='/folderUpload.svg' className={styles.grayButtonImg} />
							</GrayButton>
							<input hidden type='file' onChange={handleFileChange} />
						</Box>
					</Box>
					<Box className={styles.btns}>
						<GrayButton
							disabled={!textComment}
							onClick={() => {
								setTextComment('')
								setFileRef('')
							}}
						>
							Отмена
						</GrayButton>
						<GrayButton
							disabled={!textComment}
							onClick={() => mutationSendComment.mutate()}
						>
							Отправить
						</GrayButton>
					</Box>
				</Box>
			</Box>
		)
	}
)

export default ListTask
