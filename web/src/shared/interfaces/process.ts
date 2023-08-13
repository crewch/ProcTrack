import { Hold } from './hold'

export interface Process {
	id: number
	title: string
	priority: string
	description: string
	status: 'в процессе' | 'завершен' | 'остановлен' | 'в доработке'
	type: string
	approvedAt: string
	createdAt: string
	completedAt: string
	completedAtUnparsed: string
	expectedTime: string
	hold: Hold[]
}
