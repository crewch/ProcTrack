import axios from 'axios'
import {
	IDataForSend,
	IPriority,
	ITemplate,
} from '../interfaces/IApi/IAddProcessApi'
import { IGroup, IUser } from '../interfaces/IApi/IApi'
import { URL } from './URL/URL'

const URL_Template = `http://${URL}/api/track/property/templates`
const URL_Group = `http://${URL}/api/auth/user/groups`
const URL_Priority = `http://${URL}/api/track/property/priorities`
const URL_AddProcess = `http://${URL}/api/track/process/create`

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
			const userDataString = localStorage.getItem('UserData')

			if (userDataString) {
				const userData: IUser = JSON.parse(userDataString)

				await axios.post(URL_AddProcess, data, {
					params: {
						UserId: userData.id,
					},
				})
			}
		} catch (error) {
			if (error instanceof Error) {
				console.log(error)
			}
		}
	},
}
