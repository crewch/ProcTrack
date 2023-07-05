export interface IProcess {
	name: string
	status: 'inprogress' | 'completed' | 'rejected'
}
