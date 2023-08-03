import { configureStore } from '@reduxjs/toolkit'
import processStageReducer from './processStageSlice/processStageSlice'
import filterProcessReducer from './filterProcessSlice/filterProcessSlice'
import filterStageReducer from './filterStageSlice/filterStageSlice'

export const store = configureStore({
	reducer: {
		processStage: processStageReducer,
		filterProcess: filterProcessReducer,
		filterStages: filterStageReducer,
	},
})

export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch
