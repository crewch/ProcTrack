import axios from 'axios'
import { URL } from '../configs/url'
import { Comment } from '../shared/interfaces/comment'

const URL_commentsCreate = `http://${URL}/api/track/task/`

export const commentService = {
	async sendComment(openedTaskID: number | undefined, commentsData: Comment) {
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
