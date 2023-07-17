import { ReactNode } from 'react'
import { ISelectedStageProps } from '../../ISelectedStage/ISelectedStage'

export interface IDialogProps extends ISelectedStageProps {
	title: string
	icon: string
	children?: ReactNode
}
