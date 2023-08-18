import axios from 'axios'
import { URL } from '@/configs/url'
import { Task } from '@/shared/interfaces/task'
import { getToken } from '@/utils/getToken'

const URL_TaskAll = `${URL}/api/track/stage/`
const URL_switchTaskId = `${URL}/api/track/task/`

export const taskService = {
	async getTaskAll(id: number | undefined) {
		try {
			if (typeof id === 'number') {
				const data: Task[] = await (
					await axios.get(`${URL_TaskAll}${id}/tasks`, {
						headers: {
							authorization: `Bearer ${getToken().accessToken}`,
							Accept: 'application/json',
							'Content-Type': 'application/json',
						},
					})
				).data

				return data
			}
		} catch (error) {
			if (error instanceof Error) {
				console.log(error)
			}
		}
	},
	async startTaskId(openedTaskID: number | undefined) {
		try {
			if (typeof openedTaskID === 'undefined') return

			await (
				await axios.get(`${URL_switchTaskId}${openedTaskID}/start`, {
					headers: {
						authorization: `Bearer ${getToken().accessToken}`,
						Accept: 'application/json',
						'Content-Type': 'application/json',
					},
				})
			).data
		} catch (error) {
			if (error instanceof Error) {
				console.log(error.message)
			}
		}
	},
	async stopTaskId(openedTaskID: number | undefined) {
		try {
			if (typeof openedTaskID === 'undefined') return

			await (
				await axios.get(`${URL_switchTaskId}${openedTaskID}/stop`, {
					headers: {
						authorization: `Bearer ${getToken().accessToken}`,
						Accept: 'application/json',
						'Content-Type': 'application/json',
					},
				})
			).data
		} catch (error) {
			if (error instanceof Error) {
				console.log(error.message)
			}
		}
	},
	async endVerificationTaskId(openedTaskID: number | undefined) {
		try {
			if (typeof openedTaskID === 'undefined') return

			await (
				await axios.get(`${URL_switchTaskId}${openedTaskID}/endverification`, {
					headers: {
						authorization: `Bearer ${getToken().accessToken}`,
						Accept: 'application/json',
						'Content-Type': 'application/json',
					},
				})
			).data
		} catch (error) {
			if (error instanceof Error) {
				console.log(error.message)
			}
		}
	},
	async assignTaskId(openedTaskID: number | undefined) {
		try {
			if (typeof openedTaskID === 'undefined') return

			await (
				await axios.get(`${URL_switchTaskId}${openedTaskID}/assign`, {
					headers: {
						authorization: `Bearer ${getToken().accessToken}`,
						Accept: 'application/json',
						'Content-Type': 'application/json',
					},
				})
			).data
		} catch (error) {
			if (error instanceof Error) {
				console.log(error.message)
			}
		}
	},
}
