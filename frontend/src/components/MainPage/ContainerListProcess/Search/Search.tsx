import { Box, IconButton, InputAdornment, TextField } from '@mui/material'
import { ISearchProps } from '../../../../interfaces/IMainPage/IContainerListProcess/ISearch'
import { FC } from 'react'
import search from '../../../../assets/search.svg'
import searchSetting from '/src/assets/searchSetting.svg'
import searchSettingBlue from '/src/assets/searchSettingBlue.svg'

const Search: FC<ISearchProps> = ({
	isOpen,
	setIsOpen,
	textForSearchProcess,
	setTextForSearchProcess,
}) => {
	return (
		<Box sx={{ display: 'flex', alignItems: 'center', mb: 1 }}>
			<TextField
				value={textForSearchProcess}
				onChange={event => setTextForSearchProcess(event.target.value)}
				placeholder='Поиск...'
				autoComplete='off'
				variant='outlined'
				InputProps={{
					sx: {
						borderRadius: '8px',
						backgroundColor: '#E4E4E4',
					},
					startAdornment: (
						<InputAdornment position='start'>
							<img src={search} height='25px' width='25px' />
						</InputAdornment>
					),
				}}
				sx={{
					width: '100%',
				}}
			/>
			<IconButton
				onClick={() => setIsOpen(!isOpen)}
				sx={{
					backgroundColor: isOpen ? '#d8ecff' : 'transparent',
					color: isOpen ? '#1a2d67' : 'transparent',
					ml: '8px',
				}}
			>
				{isOpen ? (
					<img src={searchSettingBlue} height='25px' width='25px' />
				) : (
					<img src={searchSetting} height='25px' width='25px' />
				)}
			</IconButton>
			<IconButton onClick={() => setTextForSearchProcess('')}>
				<img src={search} height='25px' width='25px' />
			</IconButton>
		</Box>
	)
}

export default Search
