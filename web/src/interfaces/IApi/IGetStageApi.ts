import { IHold, IUser } from './IApi'

export interface IStage {
	id: number
	title: string
	createdAt: string
	status: string
	statusValue: number
	user: IUser
	holds: IHold[]
}
