import axios from 'axios'
import { URL } from '../configs/url'
import { Task } from '../shared/interfaces/task'

const URL_TaskAll = `http://${URL}/api/track/stage/`
const URL_switchTaskId = `http://${URL}/api/track/task/`

export const taskService = {
	async getTaskAll(id: number | undefined) {
		try {
			if (typeof id === 'number') {
				const data: Task[] = await (
					await axios.get(`${URL_TaskAll}${id}/tasks`)
				).data

				return data
			}

			return null
		} catch (error) {
			if (error instanceof Error) {
				console.log(error)
			}
		}
	},
	async startTaskId(
		openedTaskID: number | undefined,
		userId: number | undefined
	) {
		try {
			if (typeof openedTaskID === 'undefined') return null

			const data = await (
				await axios.get(`${URL_switchTaskId}${openedTaskID}/start`, {
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
				await axios.get(`${URL_switchTaskId}${openedTaskID}/stop`, {
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
				await axios.get(`${URL_switchTaskId}${openedTaskID}/endverification`, {
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
				await axios.get(`${URL_switchTaskId}${openedTaskID}/assign`, {
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
