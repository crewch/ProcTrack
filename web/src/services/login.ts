import axios from 'axios'
import { LoginForm } from '@/shared/interfaces/loginForm'
import { URL } from '@/configs/url'
import { Token } from '@/shared/interfaces/token'
import { getToken } from '@/utils/getToken'

const URL_LOGIN = `${URL}/api/auth/login`
const URL_REFRESH = `${URL}/api/auth/login/refresh`

export const loginService = {
	async login(data: LoginForm) {
		const token: Token = await (await axios.post(URL_LOGIN, data)).data
		return token
	},
	async refreshToken() {
		try {
			const token: Token = await (
				await axios.post(URL_REFRESH, getToken())
			).data

			localStorage.setItem('TOKEN', JSON.stringify(token))
		} catch (error) {
			if (error instanceof Error) {
				console.log(error.message)
			}
		}
	},
}
