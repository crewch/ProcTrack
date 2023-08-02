import { Box, IconButton, TextField } from '@mui/material'
import { Dispatch, FC, SetStateAction, memo } from 'react'
import styles from './SearchStage.module.scss'
import { useAppDispatch, useAppSelector } from '../../../../hooks/reduxHooks'
import { changeTextStage } from '../../../../store/settingStageSlice/settingStageSlice'
import SettingsImg from '../../SettingsImg/SettingsImg'

interface SearchProps {
	isOpen: boolean
	setIsOpen: Dispatch<SetStateAction<boolean>>
}

const SearchStage: FC<SearchProps> = memo(({ isOpen, setIsOpen }) => {
	const dispatch = useAppDispatch()
	const text = useAppSelector(state => state.settingStages.text)

	return (
		<Box className={styles.container}>
			<TextField
				value={text}
				onChange={event => dispatch(changeTextStage(event.target.value))}
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

export default SearchStage
