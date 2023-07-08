import { FC, memo } from 'react'
import { IListTaskProps } from '../../../../../interfaces/IMainPage/ISelectedStage/IListTasks/IListTask/IListTaskProps'
import {
	Box,
	Divider,
	List,
	ListItem,
	ListItemIcon,
	ListItemText,
	Typography,
} from '@mui/material'
import DateInfo from './DataInfo/DataInfo'
import styles from '/src/styles/MainPageStyles/SelectedStageStyles/ListTasksStyles/ListTaskStyles/ListTask.module.scss'
import UserField from '../../../SelectedProcess/InfoProcess/UserField/UserField'

const ListTask: FC<IListTaskProps> = memo(
	({ startDate, endDate, successDate, roleAuthor, author, group, remarks }) => {
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
			</Box>
		)
	}
)

export default ListTask
