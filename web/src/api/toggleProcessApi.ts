import axios from 'axios'
import { URL } from './URL/URL'

const URL_StopProcess = `http://${URL}/api/track/process/`

export const toggleProcessApi = {
	async startProcessId(openedProcessID: number | undefined) {
		try {
			if (typeof openedProcessID === 'undefined') return null

			const data = await (
				await axios.get(`${URL_StopProcess}${openedProcessID}/start`)
			).data

			return data
		} catch (error) {
			if (error instanceof Error) {
				console.log(error.message)
			}
		}
	},
	async stopProcessId(openedProcessID: number | undefined) {
		try {
			if (typeof openedProcessID === 'undefined') return null

			const data = await (
				await axios.get(`${URL_StopProcess}${openedProcessID}/stop`)
			).data

			return data
		} catch (error) {
			if (error instanceof Error) {
				console.log(error.message)
			}
		}
	},
}
