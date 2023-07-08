import { ChangeEvent, FC, memo, useState } from 'react'
import { IListTaskProps } from '../../../../../interfaces/IMainPage/ISelectedStage/IListTasks/IListTask/IListTaskProps'
import {
	Box,
	Divider,
	List,
	ListItem,
	ListItemIcon,
	ListItemText,
	TextField,
	Typography,
} from '@mui/material'
import DateInfo from './DataInfo/DataInfo'
import styles from '/src/styles/MainPageStyles/SelectedStageStyles/ListTasksStyles/ListTaskStyles/ListTask.module.scss'
import UserField from '../../../SelectedProcess/InfoProcess/UserField/UserField'
import { CustomButton } from '../../../../CustomButton/CustomButton'
import { useMutation, useQueryClient } from '@tanstack/react-query'
import { commentsApi } from '../../../../../api/commentsApi'
import { IComment } from '../../../../../interfaces/IApi/IGetTask'

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
	}) => {
		const [textComment, setTextComment] = useState('')
		const [fileRef, setFileRef] = useState('')

		const handleFileChange = (e: ChangeEvent<HTMLInputElement>) => {
			if (e.target.files) {
				const file = e.target.files[0]
				const formData = new FormData()
				formData.append('file', file)

				const getData = async () => {
					const data = await commentsApi.sendFile(formData)

					setFileRef(data)
				}

				getData()
			}
		}

		const userDataText = localStorage.getItem('UserData')

		const comment: IComment = {
			id: 0,
			text: textComment,
			fileRef: fileRef,
			user: JSON.parse(userDataText ? userDataText : ''),
			createdAt: '',
		}

		const queryClient = useQueryClient()
		const mutation = useMutation({
			mutationFn: () => commentsApi.sendComments(taskId, comment),
			onSuccess: () => {
				queryClient.invalidateQueries({ queryKey: ['tasks'] })
				setTextComment('')
			},
		})

		return (
			<Box className={styles.container}>
				<DateInfo
					startDate={startDate}
					endData={endDate}
					success={successDate}
				/>
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
							<ListItem key={index}>
								<ListItemIcon>
									<img src='/user1.svg' className={styles.img} />
								</ListItemIcon>
								<ListItemText className={styles.reportText}>
									<Box className={styles.title}>
										<Typography>{remark.user.longName}</Typography>
										<Typography>{remark.createdAt}</Typography>
									</Box>
									<Typography>{remark.text}</Typography>
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
								className={styles.uploadBtn}
								component='span'
								variant='contained'
							>
								<img src='/folderUpload.svg' height='20px' width='20px' />
							</CustomButton>
							<input hidden type='file' onChange={handleFileChange} />
						</Box>
					</Box>
					<Box className={styles.btns}>
						<CustomButton onClick={() => setTextComment('')}>
							Отмена
						</CustomButton>
						<CustomButton onClick={() => mutation.mutate()}>
							Отправить
						</CustomButton>
					</Box>
				</Box>
			</Box>
		)
	}
)

export default ListTask
