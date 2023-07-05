import axios from 'axios'
import { UserData } from './UserData/UserData'

const URL_AllProcess = 'http://localhost:8000/api/track/process'

export const getProcessApi = {
	async getProcessAll() {
		try {
			const data = await axios.get(URL_AllProcess, {
				params: {
					UserId: UserData.id,
				},
			})

			return data
		} catch (error) {
			if (error instanceof Error) {
				console.log(error.message)
			}
		}
	},
}
