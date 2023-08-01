import { FormControlLabel, FormGroup, Typography } from '@mui/material'
import Checkbox from '@mui/material/Checkbox'
import { FC, memo } from 'react'
import styles from './SettingsCheckbox.module.scss'

const SettingsCheckbox: FC = memo(() => {
	return (
		<FormGroup className={styles.formGroup}>
			{/* {setting.otherSettings.map((otherSetting, index) => (
				<FormControlLabel
					key={index}
					label={
						<Typography className={styles.typography}>
							{otherSetting.type}
						</Typography>
					}
					control={
						<Checkbox checked={otherSetting.status} name={otherSetting.type} />
					}
				/>
			))} */}
		</FormGroup>
	)
})

export default SettingsCheckbox
