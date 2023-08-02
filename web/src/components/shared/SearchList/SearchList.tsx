import { Box, Divider } from '@mui/material'
import { FC, memo, useState } from 'react'
import ListProcess from './ListProcess/ListProcess'
import ListStages from './ListStages/ListStages'
import Buttons from './Buttons/Buttons'
import SearchProcess from './SearchProcess/SearchProcess'
import SearchStage from './SearchStage/SearchStage'
import FiltersListProcess from './FiltersListProcess/FilterListProcess'
import FiltersListStage from './FiltersListStage/FiltersListStage'
import styles from './SearchList.module.scss'

interface SearchListProps {
	page: 'release' | 'approval'
}

const SearchList: FC<SearchListProps> = memo(({ page }) => {
	const [isOpen, setIsOpen] = useState(false)

	return (
		<Box className={styles.container}>
			{page === 'release' && (
				<SearchProcess isOpen={isOpen} setIsOpen={setIsOpen} />
			)}
			{page === 'approval' && (
				<SearchStage isOpen={isOpen} setIsOpen={setIsOpen} />
			)}
			<Divider variant='middle' className={styles.divider} />
			{isOpen ? (
				<>
					{page === 'release' && <FiltersListProcess />}
					{page === 'approval' && <FiltersListStage />}
				</>
			) : (
				<>
					{page === 'release' && <ListProcess />}
					{page === 'approval' && <ListStages />}
					<Buttons page={page} />
				</>
			)}
		</Box>
	)
})

export default SearchList
