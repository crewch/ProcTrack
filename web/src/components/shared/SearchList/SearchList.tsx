import { Box, Divider } from '@mui/material'
import { FC, memo, useState } from 'react'
import SettingsList from '../SettingsList/SettingsList'
import Search from '../Search/Search'
import ListProcess from './ListProcess/ListProcess'
import StagesList from './ListStages/ListStages'
import Buttons from './Buttons/Buttons'
import styles from './SearchList.module.scss'

interface SearchListWrapProps {
	page: 'release' | 'approval'
}

const SearchList: FC<SearchListWrapProps> = memo(({ page }) => {
	const [isOpen, setIsOpen] = useState(false)
	const [textForSearchProcess, setTextForSearchProcess] = useState('')

	return (
		<Box className={styles.container}>
			<Search
				isOpen={isOpen}
				setIsOpen={setIsOpen}
				textForSearchProcess={textForSearchProcess}
				setTextForSearchProcess={setTextForSearchProcess}
			/>
			<Divider variant='middle' className={styles.divider} />
			{isOpen ? (
				<SettingsList />
			) : (
				<>
					{page === 'release' && (
						<ListProcess textForSearchProcess={textForSearchProcess} />
					)}
					{page === 'approval' && (
						<StagesList textForSearchProcess={textForSearchProcess} />
					)}
					<Buttons page={page} />
				</>
			)}
		</Box>
	)
})

export default SearchList
