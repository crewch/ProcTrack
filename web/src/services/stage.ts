import axios from 'axios'
import { URL } from '../configs/url'
import { Stage } from '../shared/interfaces/stage'

const URL_StageGetAll = `http://${URL}/api/track/process/`
const URL_IdStage = `http://${URL}/api/track/stage/`
const URL_StageGetAllStageForSuccess = `http://${URL}/api/track/stage/get`

export const stageService = {
	async getStageAll(id: number | undefined) {
		try {
			if (typeof id === 'number') {
				const data: Stage[] = await (
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

			const data: Stage = await (
				await axios.get(`${URL_IdStage}${openedStageID}`)
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
				const data: Stage[] = await (
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
	async successStage(stageId: number | undefined, userId: number) {
		try {
			if (stageId) {
				await axios.get(`${URL_IdStage}${stageId}/assign`, {
					params: {
						UserId: userId,
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
	async cancelStage(stageId: number | undefined, userId: number) {
		try {
			if (stageId) {
				await axios.get(`${URL_IdStage}${stageId}/cancel`, {
					params: {
						UserId: userId,
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
	async toggleStagePass({
		stage,
		userId,
	}: {
		stage: Stage | undefined | null
		userId: number
	}) {
		try {
			if (stage) {
				//TODO: сделать нормально стукнуть бэкендера
				const data = {
					id: 0,
					processId: 0,
					title: stage.title,
					createdAt: 'string',
					signedAt: 'string',
					approvedAt: 'string',
					status: stage.status,
					statusValue: 0,
					user: {
						id: 0,
						email: 'string',
						longName: 'string',
						shortName: 'string',
						roles: ['string'],
					},
					holds: [
						{
							id: 0,
							destId: 0,
							type: 'string',
							rights: ['string'],
							users: [
								{
									id: 0,
									email: 'string',
									longName: 'string',
									shortName: 'string',
									roles: ['string'],
								},
							],
							groups: [
								{
									id: 0,
									title: 'string',
									description: 'string',
									boss: {
										id: 0,
										email: 'string',
										longName: 'string',
										shortName: 'string',
										roles: ['string'],
									},
								},
							],
						},
					],
					canCreate: [0],
					mark: true,
					pass: !stage.pass,
				}

				await axios.put(`${URL_IdStage}${stage.id}/update`, data, {
					params: {
						UserId: userId,
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
