import axios from 'axios'
import { IStage } from '../interfaces/IApi/IGetStageApi'

const URL_StageGetAll = 'http://localhost:8000/api/track/process/'

export const getStageApi = {
	async getStageALL(id: number | undefined) {
		try {
			if (typeof id === 'number') {
				const data: IStage[] = await (
					await axios.get(`${URL_StageGetAll}${id}/stage`)
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
