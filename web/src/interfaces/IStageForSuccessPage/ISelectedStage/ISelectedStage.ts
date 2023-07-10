import { IStage } from '../../IApi/IGetStageApi'

export interface ISelectedStageChildProps {
	selectedStage: IStage | null | undefined
	isLoading: boolean
	isSuccess: boolean
}
