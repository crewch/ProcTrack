import { useMemo } from 'react'
import { User } from '../shared/interfaces/user'

export function useGetUserData() {
	const userDataText = localStorage.getItem('UserData')

	const UserData: User = useMemo(() => {
		return userDataText ? JSON.parse(userDataText) : null
	}, [userDataText])

	return UserData
}
