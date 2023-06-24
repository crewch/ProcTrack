export interface IInfoProcess {
	name: string
	status: 'в процессе' | 'заврешено'
	type: string
	importance: string
	startDate: string
	interval: string
	responsible: string
	group: string
}
