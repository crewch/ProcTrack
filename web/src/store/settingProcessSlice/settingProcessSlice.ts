import { PayloadAction, createSlice } from '@reduxjs/toolkit'
import { FilterProcess } from '../../shared/interfaces/filterProcess'

const initialState: FilterProcess = {
	text: '',
	priorities: [],
	statuses: [],
	types: [],
}

export const settingProcessSlice = createSlice({
	name: 'settingProcess',
	initialState,
	reducers: {
		changeTextProcess(state, actions: PayloadAction<string>) {
			state.text = actions.payload
		},
		toggleAllSettings(
			state,
			actions: PayloadAction<{
				settings: string[]
				type: 'priorities' | 'statuses' | 'types'
			}>
		) {
			if (
				actions.payload.settings.every(item =>
					state[actions.payload.type].includes(item)
				)
			) {
				state[actions.payload.type] = []
			} else {
				state[actions.payload.type] = actions.payload.settings
			}
		},
		toggleSetting(
			state,
			actions: PayloadAction<{
				setting: string
				type: 'priorities' | 'statuses' | 'types'
			}>
		) {
			if (state[actions.payload.type].includes(actions.payload.setting)) {
				state[actions.payload.type] = state[actions.payload.type].filter(
					item => item !== actions.payload.setting
				)
			} else {
				state[actions.payload.type].push(actions.payload.setting)
			}
		},
	},
})

export const { changeTextProcess, toggleAllSettings, toggleSetting } =
	settingProcessSlice.actions
export default settingProcessSlice.reducer
