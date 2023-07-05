interface IUser {
	id: number
	email: string
	longName: string
	shortName: string
	roles: string[]
}

interface IGroup {
	id: number
	title: string
	description: string
	boss: IUser
}

interface IHold {
	id: number
	destId: number
	type: string
	rights: string[]
	users: IUser[]
	groups: IGroup[]
}

export interface Process {
	id: number
	title: string
	priority: string
	status: 'в процессе' | 'завершен' | 'остановлен' | 'отменен' | undefined
	type: string
	createdAt: string
	approvedAt: string
	expectedTime: string
	hold: IHold[]
}
