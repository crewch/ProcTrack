import axios from 'axios'
import { IUserData } from '../interfaces/IApi/ILoginApi'
import { IFormInput } from '../interfaces/ILoginPage/IFormInput'

const URL_Login = 'http://localhost:8000/api/auth/login'

export const loginApi = {
	async login(data: IFormInput) {
		try {
			const userData: IUserData = await (await axios.post(URL_Login, data)).data
			return userData
		} catch (error) {
			if (error instanceof Error) {
				console.log(error.message)
			}
		}
	},
}
