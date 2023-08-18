import axios from 'axios'
import { URL } from '@/configs/url'
import { Process } from '@/shared/interfaces/process'
import { Group } from '@/shared/interfaces/group'
import { NewProcessForm } from '@/shared/interfaces/newProcessForm'
import { FilterProcess } from '@/shared/interfaces/filterProcess'
import { getToken } from '@/utils/getToken'

const URL_AllProcess = `${URL}/api/track/process/get`
const URL_IDProcess = `${URL}/api/track/process/`
const URL_IDProcessByIdStage = `${URL}/api/track/process/`
const URL_Template = `${URL}/api/track/property/templates`
const URL_Group = `${URL}/api/auth/user/groups`
const URL_Priority = `${URL}/api/track/property/priorities`
const URL_AddProcess = `${URL}/api/track/process/create`
const URL_ProcessCount = `${URL}/api/track/process/count`

interface Template {
	id: number
	title: string
	description: string
	priority: string
	type: string
	createdAt: string
	approvedAt: string
	expectedTime: string
	hold: null
}

type Priority = string

export const processService = {
	async getProcessAll(filters: FilterProcess, limit: number, offset: number) {
		try {
			const data: Process[] = await (
				await axios.post(URL_AllProcess, filters, {
					headers: {
						authorization: `Bearer ${getToken().accessToken}`,
						Accept: 'application/json',
						'Content-Type': 'application/json',
					},
					params: {
						limit: limit,
						offset: offset - 1,
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
	async getProcessId(openedProcessID: number | undefined) {
		try {
			if (typeof openedProcessID === 'undefined') return null

			const data: Process = await (
				await axios.get(`${URL_IDProcess}${openedProcessID}`, {
					headers: {
						authorization: `Bearer ${getToken().accessToken}`,
						Accept: 'application/json',
						'Content-Type': 'application/json',
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
	async getProcessByStageId(openedStageID: number | undefined) {
		try {
			if (typeof openedStageID === 'undefined') return null

			const data: Process = await (
				await axios.get(`${URL_IDProcessByIdStage}${openedStageID}/process`, {
					headers: {
						authorization: `Bearer ${getToken().accessToken}`,
						Accept: 'application/json',
						'Content-Type': 'application/json',
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
	async startProcessId(openedProcessID: number | undefined) {
		try {
			if (typeof openedProcessID === 'undefined') return

			await (
				await axios.get(`${URL_IDProcess}${openedProcessID}/start`, {
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
	async stopProcessId(openedProcessID: number | undefined) {
		try {
			if (typeof openedProcessID === 'undefined') return

			await (
				await axios.get(`${URL_IDProcess}${openedProcessID}/stop`, {
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
	async getTemplates() {
		try {
			const data: Template[] = await (
				await axios.get(URL_Template, {
					headers: {
						authorization: `Bearer ${getToken().accessToken}`,
						Accept: 'application/json',
						'Content-Type': 'application/json',
					},
				})
			).data

			return data
		} catch (error) {
			if (error instanceof Error) {
				console.log(error)
			}
		}
	},
	async getGroupies() {
		try {
			const data: Group[] = await (
				await axios.get(URL_Group, {
					headers: {
						authorization: `Bearer ${getToken().accessToken}`,
						Accept: 'application/json',
						'Content-Type': 'application/json',
					},
				})
			).data

			return data
		} catch (error) {
			if (error instanceof Error) {
				console.log(error)
			}
		}
	},
	async getPriorities() {
		try {
			const data: Priority[] = await (
				await axios.get(URL_Priority, {
					headers: {
						authorization: `Bearer ${getToken().accessToken}`,
						Accept: 'application/json',
						'Content-Type': 'application/json',
					},
				})
			).data

			return data
		} catch (error) {
			if (error instanceof Error) {
				console.log(error)
			}
		}
	},
	async addProcess(data: NewProcessForm) {
		try {
			await axios.post(URL_AddProcess, data, {
				headers: {
					authorization: `Bearer ${getToken().accessToken}`,
					Accept: 'application/json',
					'Content-Type': 'application/json',
				},
			})
		} catch (error) {
			if (error instanceof Error) {
				console.log(error)
			}
		}
	},
	async getCountProcess(filters: FilterProcess) {
		try {
			const countProcess: number = await (
				await axios.post(URL_ProcessCount, filters, {
					headers: {
						authorization: `Bearer ${getToken().accessToken}`,
						Accept: 'application/json',
						'Content-Type': 'application/json',
					},
				})
			).data

			return countProcess
		} catch (error) {
			if (error instanceof Error) {
				console.log(error)
			}
		}
	},
}
