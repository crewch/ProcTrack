import { FormControlLabel, FormGroup, Typography } from '@mui/material'
import Checkbox from '@mui/material/Checkbox'
import { FC, memo } from 'react'
import styles from './SettingsCheckbox.module.scss'
import { useAppDispatch, useAppSelector } from '../../../../hooks/reduxHooks'
import { toggleSetting } from '../../../../store/settingProcessSlice/settingProcessSlice'

interface SettingsCheckboxProps {
	settings: string[]
	type: 'priorities' | 'statuses' | 'types'
}

const SettingsCheckbox: FC<SettingsCheckboxProps> = memo(
	({ settings, type }) => {
		const selectedSettings = useAppSelector(state => state.settingProcess)
		const dispatch = useAppDispatch()

		return (
			<FormGroup className={styles.formGroup}>
				{settings.map((setting, index) => (
					<FormControlLabel
						key={index}
						label={
							<Typography className={styles.typography}>{setting}</Typography>
						}
						control={
							<Checkbox
								checked={selectedSettings[type].includes(setting)}
								onChange={() => dispatch(toggleSetting({ setting, type }))}
								name={setting}
							/>
						}
					/>
				))}
			</FormGroup>
		)
	}
)

export default SettingsCheckbox
