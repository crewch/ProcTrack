export interface NewProcessForm {
	templateId: number | undefined
	groupId: number | undefined
	process: {
		id: number
		title: string
		description: string
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
