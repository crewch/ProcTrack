import { Box, IconButton, TextField } from '@mui/material'
import { Dispatch, FC, SetStateAction, memo } from 'react'
import styles from './Search.module.scss'

interface SearchProps {
	textForSearchProcess: string
	setTextForSearchProcess: Dispatch<SetStateAction<string>>
	isOpen: boolean
	setIsOpen: Dispatch<SetStateAction<boolean>>
}

const Search: FC<SearchProps> = memo(
	({ isOpen, setIsOpen, textForSearchProcess, setTextForSearchProcess }) => {
		return (
			<Box className={styles.container}>
				<TextField
					value={textForSearchProcess}
					onChange={event => setTextForSearchProcess(event.target.value)}
					placeholder='Поиск...'
					autoComplete='off'
					variant='standard'
					InputProps={{
						className: styles.InputProps,
						disableUnderline: true,
					}}
					className={styles.TextField}
				/>
				<IconButton
					onClick={() => setIsOpen(!isOpen)}
					className={isOpen ? styles.openSettings : styles.closeSettings}
				>
					{isOpen ? (
						<img src='/searchSettingBlue.svg' height='25px' width='25px' />
					) : (
						<img src='/searchSetting.svg' height='25px' width='25px' />
					)}
				</IconButton>
			</Box>
		)
	}
)

export default Search
