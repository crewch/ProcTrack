import { IUser } from './IApi'

export interface IComment {
	id: number
	text: string
	user: IUser
	createdAt: string
}

export interface ITask {
	id: number
	title: string
	stageId: number
	signId: number
	startedAt: string
	approvedAt: string
	expectedTime: string
	signed: string
	user: IUser
	comments: IComment[]
	endVerificationDate: string
}
