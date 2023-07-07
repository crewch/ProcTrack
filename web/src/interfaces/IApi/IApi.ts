export interface IUser {
	id: number
	email: string
	longName: string
	shortName: string
	roles: string[]
}

export interface IGroup {
	id: number
	title: string
	description: string
	boss: IUser
}

export interface IHold {
	id: number
	destId: number
	type: string
	rights: string[]
	users: IUser[]
	groups: IGroup[]
}

export interface IProcess {
	id: number
	title: string
	priority: string
	status: 'в процессе' | 'завершен' | 'остановлен' | 'отменен'
	type: string
	createdAt: string
	approvedAt: string
	expectedTime: string
	CompletedAt: string
	hold: IHold[]
}
