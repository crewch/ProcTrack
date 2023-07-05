import axios from 'axios'
import { UserData } from './UserData/UserData'
import {
	IDataForSend,
	IGroup,
	IPriority,
	ITemplate,
} from '../interfaces/IApi/IAddProcessApi'

const URL_Template = 'http://localhost:8000/api/track/property/templates'
const URL_Group = 'http://localhost:8000/api/auth/user/groups'
const URL_Priority = 'http://localhost:8000/api/track/property/priorities'
const URL_AddProcess = 'http://localhost:8000/api/track/process/create'

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
