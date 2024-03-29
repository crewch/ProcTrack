import { FilterProcess } from '@/shared/interfaces/filterProcess'
import { PayloadAction, createSlice } from '@reduxjs/toolkit'

const initialState: FilterProcess = {
	text: '',
	priorities: [],
	statuses: [],
	types: [],
	showCompleted: false,
}

export const filterProcessSlice = createSlice({
	name: 'filterProcess',
	initialState,
	reducers: {
		changeTextProcess(state, actions: PayloadAction<string>) {
			state.text = actions.payload
		},
		toggleAllFilters(
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
		toggleFilter(
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
		toggleShowCompleted(state) {
			state.showCompleted = !state.showCompleted
		},
	},
})

export const {
	changeTextProcess,
	toggleAllFilters,
	toggleFilter,
	toggleShowCompleted,
} = filterProcessSlice.actions
export default filterProcessSlice.reducer
