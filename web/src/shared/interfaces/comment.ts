import { User } from './user'

export interface Comment {
	id: number
	text: string
	fileRef: string
	user: User
	createdAt: string
}
