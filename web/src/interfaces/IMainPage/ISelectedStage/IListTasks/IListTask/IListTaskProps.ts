import { IComment } from '../../../../IApi/IGetTask'

export interface IListTaskProps {
	startDate: string
	endDate: string
	successDate: string
	roleAuthor: string
	author: string
	group: string
	remarks: IComment[]
}
