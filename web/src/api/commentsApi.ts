import axios from 'axios'
import { IComment } from '../interfaces/IApi/IGetTask'

const URL_commentsCreate = 'http://localhost:8000/api/track/task/'
const URL_sendFile = 'http://localhost:8002/upload'

export const commentsApi = {
	async sendComments(openedTaskID: number | undefined, commentsData: IComment) {
		try {
			if (typeof openedTaskID === 'undefined') return null

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
	async sendFile(file: FormData) {
		try {
			const data = await (await axios.post(URL_sendFile, file)).data

			console.log(data.fileName)
			return data.fileName
		} catch (error) {
			if (error instanceof Error) {
				console.log(error.message)
			}
		}
	},
}
