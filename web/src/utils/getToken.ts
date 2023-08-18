import { Token } from '@/shared/interfaces/token'

export function getToken() {
	const TokenString = localStorage.getItem('TOKEN')
	const Token: Token = TokenString && JSON.parse(TokenString)

	return Token
}
