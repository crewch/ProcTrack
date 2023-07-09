import { ReactNode } from 'react'
import { IContainerListProcessProps } from '../IContainerListProcess'

export interface IDialogProps extends IContainerListProcessProps {
	title: string
	icon: string
	children?: ReactNode
}
