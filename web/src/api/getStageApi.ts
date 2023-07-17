import axios from 'axios'
import { IStage } from '../interfaces/IApi/IGetStageApi'
import { URL } from './URL/URL'

const URL_StageGetAll = `http://${URL}/api/track/process/`
const URL_IDStage = `http://${URL}/api/track/stage/`
const URL_StageGetAllStageForSuccess = `http://${URL}/api/track/stage/get`

export const getStageApi = {
	async getStageAll(id: number | undefined) {
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
	async getStageAllStageForSuccess(id: number | undefined) {
		try {
			if (typeof id === 'number') {
				const data: IStage[] = await (
					await axios.get(`${URL_StageGetAllStageForSuccess}`, {
						params: {
							UserId: id,
						},
					})
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
