import { PayloadAction, createSlice } from '@reduxjs/toolkit'
import { IChangeProcess } from '../../interfaces/IStore/IProcessSlice/IActions/IChangeProcess'
import { IInitialState } from '../../interfaces/IStore/IProcessSlice/IInitialState/IInitialState'

export const processesSlice = createSlice({
	name: 'process',
	initialState: {
		openedProcess: undefined,
		openedStage: undefined,
	} as IInitialState,
	reducers: {
		changeOpenedProcess(state, actions: PayloadAction<IChangeProcess>) {
			state.openedProcess = actions.payload.id
			state.openedStage = undefined
		},
		changeOpenedStage(state, actions: PayloadAction<IChangeProcess>) {
			state.openedStage = actions.payload.id
		},
		reset(state) {
			state.openedProcess = undefined
			state.openedStage = undefined
		},
	},
})

export const { changeOpenedProcess, changeOpenedStage, reset } =
	processesSlice.actions
export default processesSlice.reducer
