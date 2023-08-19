import axios, { AxiosError } from 'axios'
import { URL } from '@/configs/url'
import { CommentCreate } from '@/shared/interfaces/comment'
import { getToken } from '@/utils/getToken'
import { loginService } from './login'

const URL_sendComment = `${URL}/api/track/task/`

export const commentService = {
	async sendComment(
		openedTaskID: number,
		commentsData: CommentCreate,
		countRepeat = 0
	) {
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
			if (countRepeat < 2 && (error as AxiosError).response?.status === 401) {
				await loginService.refreshToken()
				await this.sendComment(openedTaskID, commentsData, countRepeat + 1)
			}

			if (error instanceof Error) {
				console.log(error.message)
			}
		}
	},
}
