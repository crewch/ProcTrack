import { Pagination } from '@mui/material'
import { FC } from 'react'
import styles from './PaginationList.module.scss'

interface PaginationListProps {
	count: number
	selectedPage: number
	setSelectedPage: (value: React.SetStateAction<number>) => void
}

const PaginationList: FC<PaginationListProps> = ({
	count,
	selectedPage,
	setSelectedPage,
}) => {
	return (
		<Pagination
			count={count}
			page={selectedPage}
			onChange={(_e, page) => setSelectedPage(page)}
			color='primary'
			variant='outlined'
			showFirstButton
			showLastButton
			className={styles.pagination}
		/>
	)
}

export default PaginationList
