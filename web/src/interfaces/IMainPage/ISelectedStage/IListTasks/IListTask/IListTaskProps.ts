import { IComment } from '../../../../IApi/IGetTask'
import { ISelectedStageProps } from '../../ISelectedStage'

export interface IListTaskProps extends ISelectedStageProps {
	startDate: string
	endDate: string
	successDate: string
	roleAuthor: string
	author: string
	group: string
	remarks: IComment[]
	taskId: number
}
