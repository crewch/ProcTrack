import axios from 'axios'
import { LoginForm } from '@/shared/interfaces/loginForm'
import { URL } from '@/configs/url'
import { Token } from '@/shared/interfaces/token'

const URL_LOGIN = `${URL}/api/auth/login`

export const loginService = {
	async login(data: LoginForm) {
		const Token: Token = await (await axios.post(URL_LOGIN, data)).data
		return Token
	},
}
