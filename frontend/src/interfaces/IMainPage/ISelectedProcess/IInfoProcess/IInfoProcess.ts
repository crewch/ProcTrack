export interface IInfoProcess {
	nameOfProcess: string
	statusOfProcess: 'в процессе' | 'заврешено' | 'отклонено'
	typeOfProcess: string
	importanceOfProcess: string
	startDateOfProcess: string
	intervalOfProcess: string
	responsible: string
	group: string
}
