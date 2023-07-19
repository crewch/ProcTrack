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
import SettingsCheckbox from './SettingsCheckbox/SettingsCheckbox'
import { useAppDispatch, useAppSelector } from '../../../../../hooks/reduxHooks'
import { toggleState } from '../../../../../store/searchSettingsSlice/searchSettingsSlice'
import styles from '/src/styles/MainPageStyles/ContainerListProcessStyles/SearchStyles/SettingsListStyles/SettingsList.module.scss'

const SettingsList = () => {
	const searchSettings = useAppSelector(
		state => state.searchSettings.searchSettingsList
	)
	const dispatch = useAppDispatch()

	return (
		<List component='nav' className={styles.list}>
			{searchSettings.map((setting, index) => (
				<Accordion disableGutters key={index} className={styles.accordion}>
					<AccordionSummary expandIcon={<ExpandMoreIcon />}>
						<FormControlLabel
							key={index}
							label={
								<Typography className={styles.typography}>
									{setting.mainSetting.type}
								</Typography>
							}
							onClick={event => event.stopPropagation()}
							onFocus={event => event.stopPropagation()}
							control={
								<Checkbox
									checked={setting.mainSetting.status}
									onChange={() =>
										dispatch(toggleState({ type: setting.mainSetting.type }))
									}
									name={setting.mainSetting.type}
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
