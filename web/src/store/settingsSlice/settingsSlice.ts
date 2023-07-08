import { PayloadAction, createSlice } from '@reduxjs/toolkit'
import { settingsList } from '../../components/MainPage/ContainerListProcess/settingsList/settingsList'
import { IInitialState } from '../../interfaces/IStore/ISettingsSlice/IInitialState/IInitialState'
import { IToggleStateProps } from '../../interfaces/IStore/ISettingsSlice/IActions/IToggleState'

export const settingsSlice = createSlice({
	name: 'settings',
	initialState: { settingsList, settingsForSearch: [] } as IInitialState,
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
					break
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

			if (state.settingsList[0].otherSettings[0].status) {
				state.settingsForSearch.push('Высокая важность')
			} else {
				state.settingsForSearch = state.settingsForSearch.filter(
					item => item !== 'Высокая важность'
				)
			}

			if (state.settingsList[0].otherSettings[1].status) {
				state.settingsForSearch.push('Средняя важность')
			} else {
				state.settingsForSearch = state.settingsForSearch.filter(
					item => item !== 'Средняя важность'
				)
			}

			if (state.settingsList[0].otherSettings[2].status) {
				state.settingsForSearch.push('Низкая важность')
			} else {
				state.settingsForSearch = state.settingsForSearch.filter(
					item => item !== 'Низкая важность'
				)
			}

			if (state.settingsList[1].otherSettings[0].status) {
				state.settingsForSearch.push('КД')
			} else {
				state.settingsForSearch = state.settingsForSearch.filter(
					item => item !== 'КД'
				)
			}

			if (state.settingsList[2].otherSettings[0].status) {
				state.settingsForSearch.push('в процессе')
			} else {
				state.settingsForSearch = state.settingsForSearch.filter(
					item => item !== 'в процессе'
				)
			}

			if (state.settingsList[2].otherSettings[1].status) {
				state.settingsForSearch.push('завершен')
			} else {
				state.settingsForSearch = state.settingsForSearch.filter(
					item => item !== 'завершен'
				)
			}

			if (state.settingsList[2].otherSettings[2].status) {
				state.settingsForSearch.push('остановлен')
			} else {
				state.settingsForSearch = state.settingsForSearch.filter(
					item => item !== 'остановлен'
				)
			}

			if (state.settingsList[2].otherSettings[3].status) {
				state.settingsForSearch.push('отменен')
			} else {
				state.settingsForSearch = state.settingsForSearch.filter(
					item => item !== 'отменен'
				)
			}

			state.settingsForSearch = Array.from(new Set(state.settingsForSearch))
		},
	},
})

export const { toggleState } = settingsSlice.actions
export default settingsSlice.reducer
