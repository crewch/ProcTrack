import { IProcess } from '../../IApi/IApi'

export interface ISelectedProcessChildProps {
	selectedProcess: IProcess | null | undefined
	isLoading: boolean
	isSuccess: boolean
}
