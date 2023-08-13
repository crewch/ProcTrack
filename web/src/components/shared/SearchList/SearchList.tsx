import { Box, Divider } from '@mui/material'
import { FC, memo, useState } from 'react'
import ListProcess from './List/ListProcess'
import ListStages from './List/ListStages'
import Buttons from './Buttons/Buttons'
import SearchStage from './Search/SearchStage'
import SearchProcess from './Search/SearchProcess'
import FiltersListProcess from './FiltersList/FiltersListProcess'
import FiltersListStage from './FiltersList/FiltersListStage'
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
