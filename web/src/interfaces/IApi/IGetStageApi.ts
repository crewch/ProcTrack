import { IHold, IUser } from './IApi'

export interface IStage {
	id: number
	title: string
	createdAt: string
	canCreate: number[]
	signedAt: string
	approvedAt: string
	mark: boolean
	pass: boolean
	status: string
	statusValue: number
	user: IUser
	holds: IHold[]
}
