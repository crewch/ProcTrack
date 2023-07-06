import axios from 'axios'
import { Process } from '../interfaces/IApi/IGetProcessApi'
import { IUserData } from '../interfaces/IApi/ILoginApi'

const URL_AllProcess = 'http://localhost:8000/api/track/process/get'
const URL_IDProcess = 'http://localhost:8000/api/track/process/'

export const getProcessApi = {
	async getProcessAll() {
		try {
			const userDataString = localStorage.getItem('UserData')

			if (userDataString) {
				const userData: IUserData = JSON.parse(userDataString)

				const data: Process[] = await (
					await axios.get(URL_AllProcess, {
						params: {
							UserId: userData.id,
						},
					})
				).data

				return data
			}
		} catch (error) {
			console.log(localStorage.getItem('UserData'), 'UserData')

			if (error instanceof Error) {
				console.log(error.message)
			}
		}
	},
	async getProcessId(openedProcessID: number | undefined) {
		try {
			if (typeof openedProcessID === 'undefined') return null

			const data: Process = await (
				await axios.get(`${URL_IDProcess}${openedProcessID}`)
			).data

			return data
		} catch (error) {
			if (error instanceof Error) {
				console.log(error.message)
			}
		}
	},
}
