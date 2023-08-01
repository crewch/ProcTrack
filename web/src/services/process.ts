import axios from 'axios'
import { URL } from '../configs/url'
import { Process } from '../shared/interfaces/process'
import { Group } from '../shared/interfaces/group'
import { NewProcessForm } from '../shared/interfaces/newProcessForm'
import { FilterProcess } from '../shared/interfaces/filterProcess'

const URL_AllProcess = `${URL}/api/track/process/get`
const URL_IDProcess = `${URL}/api/track/process/`
const URL_IDProcessByIdStage = `${URL}/api/track/process/`
const URL_Template = `${URL}/api/track/property/templates`
const URL_Group = `${URL}/api/auth/user/groups`
const URL_Priority = `${URL}/api/track/property/priorities`
const URL_AddProcess = `${URL}/api/track/process/create`

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
	async getProcessAll(userId: number, settings: FilterProcess) {
		try {
			const data: Process[] = await (
				await axios.post(URL_AllProcess, settings, {
					params: {
						UserId: userId,
						limit: 10,
						offset: 0,
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
				await axios.get(`${URL_IDProcess}${openedProcessID}`)
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
				await axios.get(`${URL_IDProcessByIdStage}${openedStageID}/process`)
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
				await axios.get(`${URL_IDProcess}${openedProcessID}/start`)
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
				await axios.get(`${URL_IDProcess}${openedProcessID}/stop`)
			).data
		} catch (error) {
			if (error instanceof Error) {
				console.log(error.message)
			}
		}
	},
	async getTemplates() {
		try {
			const data: Template[] = await (await axios.get(URL_Template)).data

			return data
		} catch (error) {
			if (error instanceof Error) {
				console.log(error)
			}
		}
	},
	async getGroupies() {
		try {
			const data: Group[] = await (await axios.get(URL_Group)).data

			return data
		} catch (error) {
			if (error instanceof Error) {
				console.log(error)
			}
		}
	},
	async getPriorities() {
		try {
			const data: Priority[] = await (await axios.get(URL_Priority)).data

			return data
		} catch (error) {
			if (error instanceof Error) {
				console.log(error)
			}
		}
	},
	async addProcess({ data, userId }: { data: NewProcessForm; userId: number }) {
		try {
			await axios.post(URL_AddProcess, data, {
				params: {
					UserId: userId,
				},
			})
		} catch (error) {
			if (error instanceof Error) {
				console.log(error)
			}
		}
	},
}
