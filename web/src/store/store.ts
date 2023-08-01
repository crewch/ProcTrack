import { configureStore } from '@reduxjs/toolkit'
import processStageReducer from './processStageSlice/processStageSlice'
import settingProcessReducer from './settingProcessSlice/settingProcessSlice'

export const store = configureStore({
	reducer: {
		processStage: processStageReducer,
		settingProcess: settingProcessReducer,
	},
})

export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch
