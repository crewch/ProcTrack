import { Box, IconButton, TextField } from '@mui/material'
import { ISearchProps } from '../../../../interfaces/IMainPage/IContainerListProcess/ISearch/ISearch'
import { FC, memo } from 'react'
import searchSetting from '/searchSetting.svg'
import searchSettingBlue from '/searchSettingBlue.svg'
import styles from '/src/styles/MainPageStyles/ContainerListProcessStyles/SearchStyles/Search.module.scss'

const Search: FC<ISearchProps> = memo(
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
						<img src={searchSettingBlue} height='25px' width='25px' />
					) : (
						<img src={searchSetting} height='25px' width='25px' />
					)}
				</IconButton>
			</Box>
		)
	}
)

export default Search
