import {
	Accordion,
	AccordionDetails,
	AccordionSummary,
	Checkbox,
	FormControlLabel,
	LinearProgress,
	List,
	Typography,
} from '@mui/material'
import ExpandMoreIcon from '@mui/icons-material/ExpandMore'
import styles from './SettingsListStage.module.scss'
import { useQuery } from '@tanstack/react-query'
import { settingsService } from '../../../../services/settings'
import { useAppDispatch, useAppSelector } from '../../../../hooks/reduxHooks'
import { toggleAllSettings } from '../../../../store/settingStageSlice/settingStageSlice.ts'
import SettingsCheckbox from './SettingsCheckbox/SettingsCheckbox'

const SettingsListStage = () => {
	const {
		data: settingsStage,
		isLoading,
		isSuccess,
	} = useQuery({
		queryKey: ['settingsStage'],
		queryFn: settingsService.getSettingsStage,
	})

	const dispatch = useAppDispatch()
	const selectedSettings = useAppSelector(state => state.settingStages)

	return (
		<List component='nav' className={styles.list}>
			{isLoading && <LinearProgress />}
			{isSuccess && settingsStage && (
				<Accordion disableGutters className={styles.accordion}>
					<AccordionSummary expandIcon={<ExpandMoreIcon />}>
						<FormControlLabel
							label={
								<Typography className={styles.typography}>Статус</Typography>
							}
							onClick={event => event.stopPropagation()}
							onFocus={event => event.stopPropagation()}
							control={
								<Checkbox
									checked={settingsStage.statuses.every(item =>
										selectedSettings.statuses.includes(item)
									)}
									onChange={() =>
										dispatch(
											toggleAllSettings({
												settings: settingsStage.statuses,
												type: 'statuses',
											})
										)
									}
									name='Статус'
								/>
							}
						/>
					</AccordionSummary>
					<AccordionDetails>
						<SettingsCheckbox
							settings={settingsStage.statuses}
							type='statuses'
						/>
					</AccordionDetails>
				</Accordion>
			)}
		</List>
	)
}

export default SettingsListStage
