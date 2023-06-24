import { FormControlLabel, FormGroup, Typography } from '@mui/material'
import Checkbox from '@mui/material/Checkbox'
import { FC } from 'react'
import { ISettingsList } from '../../../../interfaces/IMainPage/IContainerListProcess/ISettingsList'
import { useAppDispatch } from '../../../../hooks/reduxHooks'
import { toggleState } from '../../../../store/settingsSlice/settingsSlice'

const SettingsCheckbox: FC<{ setting: ISettingsList }> = ({ setting }) => {
	const dispatch = useAppDispatch()

	return (
		<FormGroup>
			{setting.otherSettings.map((otherSetting, index) => (
				<FormControlLabel
					key={index}
					label={
						<Typography sx={{ color: '#333333', fontSize: '14px' }}>
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
}

export default SettingsCheckbox
