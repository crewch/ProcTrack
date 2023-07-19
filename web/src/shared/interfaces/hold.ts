import { Group } from './group'
import { User } from './user'

export interface Hold {
	id: number
	destId: number
	type: string
	rights: string[]
	users: User[]
	groups: Group[]
}
