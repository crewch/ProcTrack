import { Token } from '@/shared/interfaces/token'

export function getToken() {
	const TokenString = localStorage.getItem('TOKEN')
	const token: Token = TokenString && JSON.parse(TokenString)

	return token
}
