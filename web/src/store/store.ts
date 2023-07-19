import { configureStore } from '@reduxjs/toolkit'
import searchSettingsReducer from './searchSettingsSlice/searchSettingsSlice'
import processStageReducer from './processStageSlice/processStageSlice'

export const store = configureStore({
	reducer: {
		processStage: processStageReducer,
		searchSettings: searchSettingsReducer,
	},
})

export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch
