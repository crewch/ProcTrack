import { Box, Divider } from '@mui/material'
import { FC, memo, useState } from 'react'
import SettingsListProcess from '../SettingsListProcess/SettingsListProcess'
import ListProcess from './ListProcess/ListProcess'
import StagesList from './ListStages/ListStages'
import Buttons from './Buttons/Buttons'
import styles from './SearchList.module.scss'
import SearchProcess from '../SearchProcess/SearchProcess'

interface SearchListWrapProps {
	page: 'release' | 'approval'
}

const SearchList: FC<SearchListWrapProps> = memo(({ page }) => {
	const [isOpen, setIsOpen] = useState(false)

	return (
		<Box className={styles.container}>
			{page === 'release' && (
				<SearchProcess isOpen={isOpen} setIsOpen={setIsOpen} />
			)}
			<Divider variant='middle' className={styles.divider} />
			{isOpen ? (
				<>{page === 'release' && <SettingsListProcess />}</>
			) : (
				<>
					{page === 'release' && <ListProcess />}
					{page === 'approval' && <StagesList />}
					<Buttons page={page} />
				</>
			)}
		</Box>
	)
})

export default SearchList
