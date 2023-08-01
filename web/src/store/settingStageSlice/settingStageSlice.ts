import { PayloadAction, createSlice } from '@reduxjs/toolkit'
import { FilterStage } from '../../shared/interfaces/filterStage'

const initialState: FilterStage = {
	text: '',
	statuses: [],
}

export const settingStageSlice = createSlice({
	name: 'settingStage',
	initialState,
	reducers: {
		changeTextStage(state, actions: PayloadAction<string>) {
			state.text = actions.payload
		},
		toggleAllSettings(
			state,
			actions: PayloadAction<{
				settings: string[]
				type: 'statuses'
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
				type: 'statuses'
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

export const { changeTextStage, toggleAllSettings, toggleSetting } =
	settingStageSlice.actions
export default settingStageSlice.reducer
