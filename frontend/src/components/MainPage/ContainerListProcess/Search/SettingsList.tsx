import {
	Accordion,
	AccordionDetails,
	AccordionSummary,
	Checkbox,
	FormControlLabel,
	List,
	Typography,
} from '@mui/material'
import ExpandMoreIcon from '@mui/icons-material/ExpandMore'
import SettingsCheckbox from './SettingsCheckbox'
import { useAppDispatch, useAppSelector } from '../../../../hooks/reduxHooks'
import { toggleState } from '../../../../store/settingsSlice/settingsSlice'
import { SyntheticEvent, useState } from 'react'

const SettingsList = () => {
	const [expanded, setExpanded] = useState<number | false>(false)
	const handleChange =
		(panel: number) => (_event: SyntheticEvent, isExpanded: boolean) => {
			setExpanded(isExpanded ? panel : false)
		}

	const settings = useAppSelector(state => state.settings.settingsList)
	const dispatch = useAppDispatch()

	return (
		<List
			component='nav'
			sx={{
				width: '100%',
				height: '100%',
				overflow: 'auto',
			}}
		>
			{settings.map((setting, index) => (
				<Accordion
					key={index}
					sx={{ boxShadow: 0 }}
					expanded={expanded === index}
					onChange={handleChange(index)}
				>
					<AccordionSummary expandIcon={<ExpandMoreIcon />}>
						<FormControlLabel
							key={index}
							label={
								<Typography sx={{ color: '#333333', fontSize: '14px' }}>
									{setting.mainSetting.type}
								</Typography>
							}
							aria-label='Acknowledge'
							onClick={event => event.stopPropagation()}
							onFocus={event => event.stopPropagation()}
							control={
								<Checkbox
									checked={setting.mainSetting.status}
									onChange={() =>
										dispatch(toggleState({ type: setting.mainSetting.type }))
									}
									name={setting.mainSetting.type}
									inputProps={{ 'aria-label': 'controlled' }}
								/>
							}
						/>
					</AccordionSummary>
					<AccordionDetails>
						<SettingsCheckbox setting={setting} />
					</AccordionDetails>
				</Accordion>
			))}
		</List>
	)
}

export default SettingsList
