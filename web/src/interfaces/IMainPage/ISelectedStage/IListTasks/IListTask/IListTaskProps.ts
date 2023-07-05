import { IReamark } from '../IListTasks'

export interface IListTaskProps {
	startDate: string
	endDate: string
	successDate: string
	roleAuthor: string
	author: string
	group: string
	remarks?: IReamark[]
}
