import axios from 'axios'
import { IFormInput } from '../interfaces/ILoginPage/IFormInput'
import { IUser } from '../interfaces/IApi/IApi'
import { URL } from './URL/URL'

const URL_Login = `http://${URL}/api/auth/login`

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
