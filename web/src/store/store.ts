import { configureStore } from '@reduxjs/toolkit'
import settingsReducer from '../store/settingsSlice/settingsSlice'
import processesReducer from '../store/processSlice/processSlice'

export const store = configureStore({
	reducer: {
		settings: settingsReducer,
		processes: processesReducer,
	},
})

export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch
