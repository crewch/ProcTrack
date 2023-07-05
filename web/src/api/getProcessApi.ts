import axios from 'axios'
import { UserData } from './UserData/UserData'
import { Process } from '../interfaces/IApi/IGetProcessApi'

const URL_AllProcess = 'http://localhost:8000/api/track/process/get'

export const getProcessApi = {
	async getProcessAll() {
		try {
			const data: Process[] = await (
				await axios.get(URL_AllProcess, {
					params: {
						UserId: UserData.id,
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
