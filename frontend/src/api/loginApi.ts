import axios from 'axios'
import { IUserData } from '../interfaces/IApi/ILoginApi'
import { IFormInput } from '../interfaces/ILoginPage/IFormInput'

const URL = 'http://localhost:8003/api/Auth'

export const loginApi = {
	async login(data: IFormInput) {
		try {
			const report: IUserData = await axios.post(URL, data)
			console.log(report)
			return report
		} catch (error) {
			if (error instanceof Error) {
				console.log(error.message)
			}
		}
	},
}
