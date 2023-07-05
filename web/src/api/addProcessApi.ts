import axios from 'axios'

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

interface IGroupe {
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

const URL_Template = 'http://localhost:8000/api/track/property/templates'
const URL_Group = 'http://localhost:8000/api/auth/user/groups'
const URL_Priority = 'http://localhost:8000/api/track/property/priorities'

export const templatesApi = {
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
			const data: IGroupe[] = await (await axios.get(URL_Group)).data
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
}
