import { Comment } from '../../shared/interfaces/comment'
import { User } from './user'

export interface Task {
	id: number
	title: string
	stageId: number
	signId: number
	startedAt: string
	approvedAt: string
	expectedTime: string
	signed: string
	user: User
	comments: Comment[]
	endVerificationDate: string
}
