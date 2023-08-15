import { User } from '@/shared/interfaces/user'

export function useGetUserData() {
	const userDataText = localStorage.getItem('UserData')
	const UserData: User = userDataText && JSON.parse(userDataText)

	return UserData
}
