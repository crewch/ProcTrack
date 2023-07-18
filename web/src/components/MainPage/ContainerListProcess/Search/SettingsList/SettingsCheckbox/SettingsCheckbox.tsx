import { FormControlLabel, FormGroup, Typography } from '@mui/material'
import Checkbox from '@mui/material/Checkbox'
import { FC, memo } from 'react'
import { useAppDispatch } from '../../../../../../hooks/reduxHooks.ts'
import { toggleState } from '../../../../../../store/settingsSlice/settingsSlice.ts'
import { ISettingsCheckboxProps } from '../../../../../../interfaces/IMainPage/IContainerListProcess/ISearch/ISettingsCheckbox.ts'
import styles from '/src/styles/MainPageStyles/ContainerListProcessStyles/SearchStyles/SettingsListStyles/SettingsCheckboxStyles/SettingsCheckbox.module.scss'

const SettingsCheckbox: FC<ISettingsCheckboxProps> = memo(({ setting }) => {
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
