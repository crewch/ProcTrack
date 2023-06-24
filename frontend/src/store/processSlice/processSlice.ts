import { PayloadAction, createSlice } from '@reduxjs/toolkit'
import { IChangeProcess } from '../../interfaces/IStore/IProcessSlice/IActions/IChangeProcess'

export const processesSlice = createSlice({
	name: 'process',
	initialState: {
		openedProcess: '1 процесс',
	},
	reducers: {
		changeOpenedProcess(state, actions: PayloadAction<IChangeProcess>) {
			state.openedProcess = actions.payload.name
		},
	},
})

export const { changeOpenedProcess } = processesSlice.actions
export default processesSlice.reducer
