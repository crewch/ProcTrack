import axios from 'axios'
import { IFormInput } from '../interfaces/ILoginPage/IFormInput'
import { IUser } from '../interfaces/IApi/IApi'

const URL_Login = 'http://localhost:8000/api/auth/login'

export const loginApi = {
	async login(data: IFormInput) {
		try {
			const userData: IUser = await (await axios.post(URL_Login, data)).data
			return userData
		} catch (error) {
			if (error instanceof Error) {
				console.log(error.message)
			}
		}
	},
}
