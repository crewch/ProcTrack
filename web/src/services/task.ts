import axios, { AxiosError } from 'axios'
import { URL } from '@/configs/url'
import { Task } from '@/shared/interfaces/task'
import { getToken } from '@/utils/getToken'
import { loginService } from './login'

const URL_TaskAll = `${URL}/api/track/stage/`
const URL_switchTaskId = `${URL}/api/track/task/`

export const taskService = {
	async getTaskAll(id: number | undefined, countRepeat = 0) {
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
			if (countRepeat < 2 && (error as AxiosError).response?.status === 401) {
				await loginService.refreshToken()
				await this.getTaskAll(id, countRepeat + 1)
			}

			if (error instanceof Error) {
				console.log(error)
			}
		}
	},
	async startTaskId(openedTaskID: number | undefined, countRepeat = 0) {
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
			if (countRepeat < 2 && (error as AxiosError).response?.status === 401) {
				await loginService.refreshToken()
				await this.startTaskId(openedTaskID, countRepeat + 1)
			}

			if (error instanceof Error) {
				console.log(error.message)
			}
		}
	},
	async stopTaskId(openedTaskID: number | undefined, countRepeat = 0) {
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
			if (countRepeat < 2 && (error as AxiosError).response?.status === 401) {
				await loginService.refreshToken()
				await this.stopTaskId(openedTaskID, countRepeat + 1)
			}

			if (error instanceof Error) {
				console.log(error.message)
			}
		}
	},
	async endVerificationTaskId(
		openedTaskID: number | undefined,
		countRepeat = 0
	) {
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
			if (countRepeat < 2 && (error as AxiosError).response?.status === 401) {
				await loginService.refreshToken()
				await this.endVerificationTaskId(openedTaskID, countRepeat + 1)
			}

			if (error instanceof Error) {
				console.log(error.message)
			}
		}
	},
	async assignTaskId(openedTaskID: number | undefined, countRepeat = 0) {
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
			if (countRepeat < 2 && (error as AxiosError).response?.status === 401) {
				await loginService.refreshToken()
				await this.assignTaskId(openedTaskID, countRepeat + 1)
			}

			if (error instanceof Error) {
				console.log(error.message)
			}
		}
	},
}
