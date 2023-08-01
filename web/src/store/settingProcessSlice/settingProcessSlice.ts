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
	},
})

export const { changeTextProcess } = settingProcessSlice.actions
export default settingProcessSlice.reducer
