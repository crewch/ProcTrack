import { FormControlLabel, FormGroup, Typography } from '@mui/material'
import Checkbox from '@mui/material/Checkbox'
import { FC, memo } from 'react'
import { useAppDispatch } from '../../../../hooks/reduxHooks.ts'
import { toggleState } from '../../../../store/searchSettingsSlice/searchSettingsSlice.ts'
import { SettingsItem } from '../../../../shared/interfaces/settingsItem.ts'
import styles from './SettingsCheckbox.module.scss'

interface SettingsCheckboxProps {
	setting: SettingsItem
}

const SettingsCheckbox: FC<SettingsCheckboxProps> = memo(({ setting }) => {
	const dispatch = useAppDispatch()

	return (
		<FormGroup className={styles.formGroup}>
			{setting.otherSettings.map((otherSetting, index) => (
				<FormControlLabel
					key={index}
					label={
						<Typography className={styles.typography}>
							{otherSetting.type}
						</Typography>
					}
					control={
						<Checkbox
							checked={otherSetting.status}
							onChange={() =>
								dispatch(toggleState({ type: otherSetting.type }))
							}
							name={otherSetting.type}
						/>
					}
				/>
			))}
		</FormGroup>
	)
})

export default SettingsCheckbox
