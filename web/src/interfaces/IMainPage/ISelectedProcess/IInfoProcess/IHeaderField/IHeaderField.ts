import { ISelectedStageProps } from '../../../ISelectedStage/ISelectedStage'

export interface IHeaderFieldProps extends ISelectedStageProps {
	name: string
	status: string
	importance: string
	type: string
}
