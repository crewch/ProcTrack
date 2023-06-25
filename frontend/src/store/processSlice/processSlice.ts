import { PayloadAction, createSlice } from '@reduxjs/toolkit'
import { IChangeProcess } from '../../interfaces/IStore/IProcessSlice/IActions/IChangeProcess'

export const processesSlice = createSlice({
	name: 'process',
	initialState: {
		openedProcess: '1 процесс',
		openedStage: '1 этап',
	},
	reducers: {
		changeOpenedProcess(state, actions: PayloadAction<IChangeProcess>) {
			state.openedProcess = actions.payload.name
			state.openedStage = '1 этап'
		},
		changeOpenedStage(state, actions: PayloadAction<IChangeProcess>) {
			state.openedStage = actions.payload.name
		},
	},
})

export const { changeOpenedProcess, changeOpenedStage } = processesSlice.actions
export default processesSlice.reducer
