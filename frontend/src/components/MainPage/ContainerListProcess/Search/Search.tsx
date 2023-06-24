import {
	Accordion,
	AccordionDetails,
	AccordionSummary,
	Box,
	Divider,
	FormControlLabel,
	InputAdornment,
	TextField,
} from '@mui/material'
import TuneIcon from '@mui/icons-material/Tune'
import { ISearchProps } from '../../../../interfaces/IMainPage/IContainerListProcess/ISearch'
import { FC } from 'react'
import search from '../../../../assets/search.svg'
import SettingsList from './SettingsList'
import styles from '/src/styles/MainPageStyles/ContainerListProcess/Search/Search.module.css'

const Search: FC<ISearchProps> = ({
	textForSearchProcess,
	setTextForSearchProcess,
}) => {
	return (
		<Accordion>
			<AccordionSummary
				onClick={() =>
					document
						.querySelector('.clickedButtonSettingFlag')
						?.classList.toggle(styles.clickedButtonSetting)
				}
				expandIcon={
					<Box
						className='clickedButtonSettingFlag'
						sx={{
							height: '42px',
							width: '42px',
							borderRadius: '8px',
							display: 'flex',
							justifyContent: 'center',
							alignItems: 'center',
						}}
					>
						<TuneIcon />
					</Box>
				}
			>
				<FormControlLabel
					aria-label='Acknowledge'
					onClick={event => event.stopPropagation()}
					onFocus={event => event.stopPropagation()}
					label=''
					sx={{
						width: '100%',
					}}
					control={
						<TextField
							value={textForSearchProcess}
							onChange={event => setTextForSearchProcess(event.target.value)}
							placeholder='Поиск...'
							autoComplete='off'
							variant='outlined'
							InputProps={{
								style: {
									borderRadius: '8px',
									backgroundColor: '#E4E4E4',
								},
								startAdornment: (
									<InputAdornment position='start'>
										<img src={search} />
									</InputAdornment>
								),
							}}
							sx={{
								width: '100%',
							}}
						/>
					}
				/>
			</AccordionSummary>
			<AccordionDetails>
				<Divider sx={{ mb: 1, borderWidth: 1 }} />
				<SettingsList />
			</AccordionDetails>
		</Accordion>
	)
}

export default Search
