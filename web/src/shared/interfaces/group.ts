import { User } from './user'

export interface Group {
	id: number
	title: string
	description: string
	boss: User
}
