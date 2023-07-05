export interface ITemplate {
	id: number
	title: string
	priority: string
	type: string
	createdAt: string
	approvedAt: string
	expectedTime: string
	hold: null
}

export interface IGroup {
	id: number
	title: string
	description: string
	boss: {
		id: number
		email: string
		longName: string
		shortName: string
		roles: string[]
	}
}

export type IPriority = string

export interface IDataForSend {
	templateId: number | undefined
	groupId: number | undefined
	process: {
		id: number
		title: string | undefined
		priority: string | undefined | null
		type: string
		createdAt: string
		approvedAt: string
		expectedTime: null
		hold: [
			{
				id: number
				destId: number
				type: string
				rights: string[]
				users: [
					{
						id: number
						email: string
						longName: string
						shortName: string
						roles: string[]
					}
				]
				groups: [
					{
						id: number
						title: string
						description: string
						boss: {
							id: number
							email: string
							longName: string
							shortName: string
							roles: string[]
						}
					}
				]
			}
		]
	}
}
