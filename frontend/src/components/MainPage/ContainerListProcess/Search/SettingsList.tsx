import {
	Accordion,
	AccordionDetails,
	AccordionSummary,
	Checkbox,
	FormControlLabel,
	List,
	Typography,
} from '@mui/material'
import Box from '@mui/material/Box'
import ExpandMoreIcon from '@mui/icons-material/ExpandMore'
import SettingsCheckbox from './SettingsCheckbox'
import { useAppDispatch, useAppSelector } from '../../../../hooks/reduxHooks'
import { toggleState } from '../../../../store/settingsSlice/settingsSlice'

const SettingsList = () => {
	const settings = useAppSelector(state => state.settings.settingsList)
	const dispatch = useAppDispatch()

	return (
		<Box
			sx={{
				width: '100%',
				height: 250,
			}}
		>
			<List
				sx={{
					width: '100%',
					height: '100%',
					overflow: 'auto',
				}}
			>
				{settings.map((setting, index) => (
					<Accordion key={index} component='nav' sx={{ boxShadow: 0 }}>
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
		</Box>
	)
}

export default SettingsList
