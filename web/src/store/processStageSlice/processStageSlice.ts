import { PayloadAction, createSlice } from '@reduxjs/toolkit'

interface InitialState {
	openedProcess: number | undefined
	openedStage: number | undefined
}

const initialState: InitialState = {
	openedProcess: undefined,
	openedStage: undefined,
}

interface PayloadActionProps {
	id: number
}

export const processStageSlice = createSlice({
	name: 'processStage',
	initialState,
	reducers: {
		changeOpenedProcess(state, actions: PayloadAction<PayloadActionProps>) {
			state.openedProcess = actions.payload.id
			state.openedStage = undefined
		},
		setOpenedProcess(state, actions: PayloadAction<PayloadActionProps>) {
			state.openedProcess = actions.payload.id
		},
		changeOpenedStage(state, actions: PayloadAction<PayloadActionProps>) {
			state.openedStage = actions.payload.id
		},
		reset(state) {
			state.openedProcess = undefined
			state.openedStage = undefined
		},
	},
})

export const {
	changeOpenedProcess,
	setOpenedProcess,
	changeOpenedStage,
	reset,
} = processStageSlice.actions
export default processStageSlice.reducer
