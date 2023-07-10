import axios from 'axios'
import { IStage } from '../interfaces/IApi/IGetStageApi'

const URL_StageGetAll = 'http://localhost:8000/api/track/process/'
const URL_IDStage = 'http://localhost:8000/api/track/stage/'
const URL_StageGetAllStageForSuccess =
	'http://localhost:8000/api/track/stage/get'

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
	async getStageId(openedStageID: number | undefined) {
		try {
			if (typeof openedStageID === 'undefined') return null

			const data: IStage = await (
				await axios.get(`${URL_IDStage}${openedStageID}`)
			).data

			return data
		} catch (error) {
			if (error instanceof Error) {
				console.log(error.message)
			}
		}
	},
	async getStageALLStageForSuccess(id: number | undefined) {
		try {
			if (typeof id === 'number') {
				const data: IStage[] = await (
					await axios.get(`${URL_StageGetAllStageForSuccess}`, {
						params: {
							UserId: id,
						},
					})
				).data

				console.log(data)

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
