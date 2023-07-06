import axios from 'axios'

const URL_StopProcess = 'http://localhost:8000/api/track/process/'

export const startProcessApi = {
	async stopProcessId(openedProcessID: number | undefined) {
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
}
