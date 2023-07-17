import axios from 'axios'
import { ITask } from '../interfaces/IApi/IGetTask'
import { URL } from './URL/URL'

const URL_TaskAll = `http://${URL}/api/track/stage/`

export const getTaskApi = {
	async getTaskAll(id: number | undefined) {
		try {
			if (typeof id === 'number') {
				const data: ITask[] = await (
					await axios.get(`${URL_TaskAll}${id}/tasks`)
				).data

				return data
			}

			return null
		} catch (error) {
			if (error instanceof Error) {
				console.log(error)
			}
		}
	},
}
