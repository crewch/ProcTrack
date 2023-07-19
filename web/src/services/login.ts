import axios from 'axios'
import { LoginForm } from '../shared/interfaces/loginForm'
import { URL } from '../configs/url'
import { User } from '../shared/interfaces/user'

const URL_Login = `http://${URL}/api/auth/login`

export const loginService = {
	async login(data: LoginForm) {
		try {
			const userData: User = await (await axios.post(URL_Login, data)).data
			return userData
		} catch (error) {
			if (error instanceof Error) {
				console.log(error.message)
			}
		}
	},
}
