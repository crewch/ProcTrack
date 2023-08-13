import { PayloadAction, createSlice } from '@reduxjs/toolkit'
import { FilterStage } from '../../shared/interfaces/filterStage'

const initialState: FilterStage = {
	text: '',
	statuses: [],
	priorities: [],
	types: [],
	showApproved: false,
}

export const filterStageSlice = createSlice({
	name: 'filterStage',
	initialState,
	reducers: {
		changeTextStage(state, actions: PayloadAction<string>) {
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
		toggleShowApproved(state) {
			state.showApproved = !state.showApproved
		},
	},
})

export const {
	changeTextStage,
	toggleAllFilters,
	toggleFilter,
	toggleShowApproved,
} = filterStageSlice.actions
export default filterStageSlice.reducer
