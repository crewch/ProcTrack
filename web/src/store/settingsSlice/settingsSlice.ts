import { PayloadAction, createSlice } from '@reduxjs/toolkit'
import { settingsList } from '../../components/MainPage/ContainerListProcess/settingsList/settingsList'
import { IInitialState } from '../../interfaces/IStore/ISettingsSlice/IInitialState/IInitialState'
import { IToggleStateProps } from '../../interfaces/IStore/ISettingsSlice/IActions/IToggleState'

export const settingsSlice = createSlice({
	name: 'settings',
	initialState: { settingsList } as IInitialState,
	reducers: {
		toggleState: (state, actions: PayloadAction<IToggleStateProps>) => {
			for (const setting of state.settingsList) {
				let flag = true

				if (setting.mainSetting.type === actions.payload.type) {
					setting.mainSetting.status = !setting.mainSetting.status

					if (setting.mainSetting.status) {
						for (const otherSetting of setting.otherSettings) {
							otherSetting.status = true
						}
					} else {
						for (const otherSetting of setting.otherSettings) {
							otherSetting.status = false
						}
					}
					return
				}

				for (const otherSetting of setting.otherSettings) {
					if (otherSetting.type === actions.payload.type) {
						otherSetting.status = !otherSetting.status
					}

					if (otherSetting.status === false) {
						flag = false
					}
				}

				if (flag) {
					setting.mainSetting.status = true
				} else {
					setting.mainSetting.status = false
				}
			}
		},
	},
})

export const { toggleState } = settingsSlice.actions
export default settingsSlice.reducer
