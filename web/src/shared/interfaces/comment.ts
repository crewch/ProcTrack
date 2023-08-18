import { User } from './user'

export interface Comment {
	id: number
	text: string
	fileRef: string
	user: User | null
	createdAt: string
}

export type CommentCreate = Omit<Comment, 'user'>
