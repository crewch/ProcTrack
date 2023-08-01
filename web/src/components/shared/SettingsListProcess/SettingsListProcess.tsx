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
import styles from './SettingsListProcess.module.scss'
import { useQuery } from '@tanstack/react-query'
import { settingsService } from '../../../services/settings'
import { useAppDispatch, useAppSelector } from '../../../hooks/reduxHooks'
import { toggleAllSettings } from '../../../store/settingProcessSlice/settingProcessSlice'
import SettingsCheckbox from './SettingsCheckbox/SettingsCheckbox'

const SettingsListProcess = () => {
	const {
		data: settingsProcess,
		isLoading,
		isSuccess,
	} = useQuery({
		queryKey: ['settingsProcess'],
		queryFn: settingsService.getSettingsProcess,
	})

	const dispatch = useAppDispatch()
	const selectedSettings = useAppSelector(state => state.settingProcess)

	return (
		<List component='nav' className={styles.list}>
			{isLoading && <LinearProgress />}
			{isSuccess && settingsProcess && (
				<>
					<Accordion disableGutters className={styles.accordion}>
						<AccordionSummary expandIcon={<ExpandMoreIcon />}>
							<FormControlLabel
								label={
									<Typography className={styles.typography}>
										Важность
									</Typography>
								}
								onClick={event => event.stopPropagation()}
								onFocus={event => event.stopPropagation()}
								control={
									<Checkbox
										checked={settingsProcess.priorities.every(item =>
											selectedSettings.priorities.includes(item)
										)}
										onChange={() =>
											dispatch(
												toggleAllSettings({
													settings: settingsProcess.priorities,
													type: 'priorities',
												})
											)
										}
										name='Важность'
									/>
								}
							/>
						</AccordionSummary>
						<AccordionDetails>
							<SettingsCheckbox
								settings={settingsProcess.priorities}
								type='priorities'
							/>
						</AccordionDetails>
					</Accordion>
					<Accordion disableGutters className={styles.accordion}>
						<AccordionSummary expandIcon={<ExpandMoreIcon />}>
							<FormControlLabel
								label={
									<Typography className={styles.typography}>Тип</Typography>
								}
								onClick={event => event.stopPropagation()}
								onFocus={event => event.stopPropagation()}
								control={
									<Checkbox
										checked={settingsProcess.types.every(item =>
											selectedSettings.types.includes(item)
										)}
										onChange={() =>
											dispatch(
												toggleAllSettings({
													settings: settingsProcess.types,
													type: 'types',
												})
											)
										}
										name='Тип'
									/>
								}
							/>
						</AccordionSummary>
						<AccordionDetails>
							<SettingsCheckbox settings={settingsProcess.types} type='types' />
						</AccordionDetails>
					</Accordion>
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
										checked={settingsProcess.statuses.every(item =>
											selectedSettings.statuses.includes(item)
										)}
										onChange={() =>
											dispatch(
												toggleAllSettings({
													settings: settingsProcess.statuses,
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
								settings={settingsProcess.statuses}
								type='statuses'
							/>
						</AccordionDetails>
					</Accordion>
				</>
			)}
		</List>
	)
}

export default SettingsListProcess
