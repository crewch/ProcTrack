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
import { FC, useState } from 'react'
import search from '../../../../assets/search.svg'
import SettingsList from './SettingsList'
import styles from '/src/styles/MainPageStyles/ContainerListProcessStyles/SearchStyles/Search.module.scss'
import searchSetting from '/src/assets/searchSetting.svg'
import searchSettingBlue from '/src/assets/searchSettingBlue.svg'

const Search: FC<ISearchProps> = ({
	textForSearchProcess,
	setTextForSearchProcess,
}) => {
	const [isOpen, setIsOpen] = useState(false)

	return (
		<Accordion
			sx={{
				boxShadow: 'none',
			}}
		>
			<AccordionSummary
				onClick={() => {
					setIsOpen(!isOpen)
					document
						.querySelector('.clickedButtonSettingFlag')
						?.classList.toggle(styles.clickedButtonSetting)
				}}
				expandIcon={
					<Box
						className={`clickedButtonSettingFlag  ${styles.clickedButtonSettingIcon}`}
					>
						{isOpen ? (
							<img src={searchSettingBlue} />
						) : (
							<img src={searchSetting} />
						)}
					</Box>
				}
			>
				<FormControlLabel
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
								sx: {
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
				<Divider className={styles.divider} />
				<SettingsList />
			</AccordionDetails>
		</Accordion>
	)
}

export default Search
