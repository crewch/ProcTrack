import axios from 'axios'
import { IProcess, IUser } from '../interfaces/IApi/IApi'

const URL_AllProcess = 'http://localhost:8000/api/track/process/get'
const URL_IDProcess = 'http://localhost:8000/api/track/process/'
const URL_IDProcessByIdStage = 'http://localhost:8000/api/track/process/'

export const getProcessApi = {
	async getProcessAll() {
		try {
			const userDataString = localStorage.getItem('UserData')

			if (userDataString) {
				const userData: IUser = JSON.parse(userDataString)

				const data: IProcess[] = await (
					await axios.get(URL_AllProcess, {
						params: {
							UserId: userData.id,
						},
					})
				).data

				return data
			}
		} catch (error) {
			if (error instanceof Error) {
				console.log(error.message)
			}
		}
	},
	async getProcessId(openedProcessID: number | undefined) {
		try {
			if (typeof openedProcessID === 'undefined') return null

			const data: IProcess = await (
				await axios.get(`${URL_IDProcess}${openedProcessID}`)
			).data

			return data
		} catch (error) {
			if (error instanceof Error) {
				console.log(error.message)
			}
		}
	},
	async getProcessByStageId(openedStageID: number | undefined) {
		try {
			if (typeof openedStageID === 'undefined') return null

			const data: IProcess = await (
				await axios.get(`${URL_IDProcessByIdStage}${openedStageID}/process`)
			).data

			return data
		} catch (error) {
			if (error instanceof Error) {
				console.log(error.message)
			}
		}
	},
}
