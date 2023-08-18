import axios from 'axios'
import { URL } from '@/configs/url'
import { Comment } from '@/shared/interfaces/comment'
import { getToken } from '@/utils/getToken'

const URL_sendComment = `${URL}/api/track/task/`

export const commentService = {
	async sendComment(openedTaskID: number, commentsData: Comment) {
		try {
			if (!commentsData.text) return

			await axios.post(
				`${URL_sendComment}${openedTaskID}/comments/create`,
				commentsData,
				{
					headers: {
						authorization: `Bearer ${getToken().accessToken}`,
						Accept: 'application/json',
						'Content-Type': 'application/json',
					},
				}
			)
		} catch (error) {
			if (error instanceof Error) {
				console.log(error.message)
			}
		}
	},
}
