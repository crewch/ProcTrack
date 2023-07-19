import { Hold } from './hold'
import { User } from './user'

export interface Stage {
	id: number
	title: string
	createdAt: string
	canCreate: number[]
	signedAt: string
	approvedAt: string
	mark: boolean
	pass: boolean
	status: string
	statusValue: number
	user: User
	holds: Hold[]
}
