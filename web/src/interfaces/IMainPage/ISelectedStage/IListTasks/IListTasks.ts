export interface IReamark {
	author: string
	date: string
	text: string
}

export interface IListTasks {
	numberOfTask: string
	startDate: string
	endDate: string
	status: string
	successDate: string
	roleAuthor: string
	author: string
	group: string
	remarks: IReamark[]
}
