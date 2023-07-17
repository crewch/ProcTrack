import axios from 'axios'
import { URL } from './URL/URL'

const URL_startTaskId = `http://${URL}/api/track/task/`

export const switchTaskApi = {
	async startTaskId(
		openedTaskID: number | undefined,
		userId: number | undefined
	) {
		try {
			if (typeof openedTaskID === 'undefined') return null

			const data = await (
				await axios.get(`${URL_startTaskId}${openedTaskID}/start`, {
					params: {
						UserId: userId,
					},
				})
			).data

			return data
		} catch (error) {
			if (error instanceof Error) {
				console.log(error.message)
			}
		}
	},
	async stopTaskId(
		openedTaskID: number | undefined,
		userId: number | undefined
	) {
		try {
			if (typeof openedTaskID === 'undefined') return null

			const data = await (
				await axios.get(`${URL_startTaskId}${openedTaskID}/stop`, {
					params: {
						UserId: userId,
					},
				})
			).data

			return data
		} catch (error) {
			if (error instanceof Error) {
				console.log(error.message)
			}
		}
	},
	async endVerificationTaskId(
		openedTaskID: number | undefined,
		userId: number | undefined
	) {
		try {
			if (typeof openedTaskID === 'undefined') return null

			const data = await (
				await axios.get(`${URL_startTaskId}${openedTaskID}/endverification`, {
					params: {
						UserId: userId,
					},
				})
			).data

			return data
		} catch (error) {
			if (error instanceof Error) {
				console.log(error.message)
			}
		}
	},
	async assignTaskId(
		openedTaskID: number | undefined,
		userId: number | undefined
	) {
		try {
			if (typeof openedTaskID === 'undefined') return null

			const data = await (
				await axios.get(`${URL_startTaskId}${openedTaskID}/assign`, {
					params: {
						UserId: userId,
					},
				})
			).data

			return data
		} catch (error) {
			if (error instanceof Error) {
				console.log(error.message)
			}
		}
	},
}
