import { Box, IconButton, TextField } from '@mui/material'
import { Dispatch, FC, SetStateAction, memo } from 'react'
import styles from './SearchProcess.module.scss'
import { useAppDispatch, useAppSelector } from '../../../../hooks/reduxHooks'
import { changeTextProcess } from '../../../../store/filterProcessSlice/filterProcessSlice'
import SettingsImg from '../../SettingsImg/SettingsImg'

interface SearchProps {
	isOpen: boolean
	setIsOpen: Dispatch<SetStateAction<boolean>>
}

const SearchProcess: FC<SearchProps> = memo(({ isOpen, setIsOpen }) => {
	const dispatch = useAppDispatch()
	const text = useAppSelector(state => state.filterProcess.text)

	return (
		<Box className={styles.container}>
			<TextField
				value={text}
				onChange={event => dispatch(changeTextProcess(event.target.value))}
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
				<SettingsImg isOpen={isOpen} />
			</IconButton>
		</Box>
	)
})

export default SearchProcess
