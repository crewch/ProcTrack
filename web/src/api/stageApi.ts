import axios from 'axios'
import { IUser } from '../interfaces/IApi/IApi'

const URL_StageAssign = 'http://localhost:8000/api/track/stage/'
const URL_StageCancel = 'http://localhost:8000/api/track/stage/'

export const stageApi = {
	async successStage(stageId: number | undefined) {
		try {
			const textUserData = localStorage.getItem('UserData')

			if (textUserData && stageId) {
				const userData: IUser = JSON.parse(textUserData)

				await axios.get(`${URL_StageAssign}${stageId}/assign`, {
					params: {
						UserId: userData.id,
					},
				})
			}

			return null
		} catch (error) {
			if (error instanceof Error) {
				console.log(error.message)
			}
		}
	},
	async cancelStage(stageId: number | undefined) {
		try {
			const textUserData = localStorage.getItem('UserData')

			if (textUserData && stageId) {
				const userData: IUser = JSON.parse(textUserData)

				await axios.get(`${URL_StageCancel}${stageId}/cancel`, {
					params: {
						UserId: userData.id,
					},
				})
			}

			return null
		} catch (error) {
			if (error instanceof Error) {
				console.log(error.message)
			}
		}
	},
}
