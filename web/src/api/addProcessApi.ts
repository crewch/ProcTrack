import axios from 'axios'
import { IUserData } from '../interfaces/IApi/ILoginApi'

interface ITemplate {
	id: number
	title: string
	priority: string
	type: string
	createdAt: string
	approvedAt: string
	expectedTime: string
	hold: null
}

interface IGroup {
	id: number
	title: string
	description: string
	boss: {
		id: number
		email: string
		longName: string
		shortName: string
		roles: string[]
	}
}

type IPriority = string

export interface IDataForSend {
	templateId: number | undefined
	groupId: number | undefined
	process: {
		id: number
		title: string | undefined
		priority: string | undefined | null
		type: string
		createdAt: string
		approvedAt: string
		expectedTime: null
		hold: [
			{
				id: number
				destId: number
				type: string
				rights: string[]
				users: [
					{
						id: number
						email: string
						longName: string
						shortName: string
						roles: string[]
					}
				]
				groups: [
					{
						id: number
						title: string
						description: string
						boss: {
							id: number
							email: string
							longName: string
							shortName: string
							roles: string[]
						}
					}
				]
			}
		]
	}
}

const URL_Template = 'http://localhost:8000/api/track/property/templates'
const URL_Group = 'http://localhost:8000/api/auth/user/groups'
const URL_Priority = 'http://localhost:8000/api/track/property/priorities'
const URL_AddProcess = 'http://localhost:8000/api/track/process/create'

const UserDataString = localStorage.getItem('UserData')
const UserData: IUserData = UserDataString && JSON.parse(UserDataString)

export const addProcessApi = {
	async getTemplates() {
		try {
			const data: ITemplate[] = await (await axios.get(URL_Template)).data
			return data
		} catch (error) {
			if (error instanceof Error) {
				console.log(error)
			}
		}
	},
	async getGroupes() {
		try {
			const data: IGroup[] = await (await axios.get(URL_Group)).data
			return data
		} catch (error) {
			if (error instanceof Error) {
				console.log(error)
			}
		}
	},
	async getPriorities() {
		try {
			const data: IPriority[] = await (await axios.get(URL_Priority)).data
			return data
		} catch (error) {
			if (error instanceof Error) {
				console.log(error)
			}
		}
	},
	async addProcess(data: IDataForSend) {
		try {
			const report = await axios.post(URL_AddProcess, data, {
				params: {
					UserId: UserData.id,
				},
			})

			console.log(report)
		} catch (error) {
			if (error instanceof Error) {
				console.log(error)
			}
		}
	},
}
