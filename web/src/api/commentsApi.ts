import axios from 'axios'
import { IComment } from '../interfaces/IApi/IGetTask'
import { URL } from './URL/URL'

const URL_commentsCreate = `http://${URL}/api/track/task/`

export const commentsApi = {
	async sendComments(openedTaskID: number | undefined, commentsData: IComment) {
		try {
			if (typeof openedTaskID === 'undefined') return null
			if (!commentsData.text) return null

			await axios.post(
				`${URL_commentsCreate}${openedTaskID}/comments/create`,
				commentsData
			)
		} catch (error) {
			if (error instanceof Error) {
				console.log(error.message)
			}
		}
	},
}
