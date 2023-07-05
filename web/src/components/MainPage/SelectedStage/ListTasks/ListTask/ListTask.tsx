import { FC } from 'react'
import { IListTaskProps } from '../../../../../interfaces/IMainPage/ISelectedStage/IListTasks/IListTask/IListTaskProps'
import { Box, Divider } from '@mui/material'
import DateInfo from './DataInfo/DataInfo'
import styles from '/src/styles/MainPageStyles/SelectedStageStyles/ListTasksStyles/ListTaskStyles/ListTask.module.scss'
import UserField from '../../../SelectedProcess/InfoProcess/UserField/UserField'

const ListTask: FC<IListTaskProps> = ({
	startDate,
	endDate,
	successDate,
	roleAuthor,
	author,
	group,
	_remarks,
}) => {
	return (
		<Box className={styles.container}>
			<DateInfo startDate={startDate} endData={endDate} success={successDate} />
			<Divider className={styles.divider} />
			<UserField group={group} responsible={author} role={roleAuthor} />
			<Divider className={styles.divider} />
		</Box>
	)
}

export default ListTask
