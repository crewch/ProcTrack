import axios, { AxiosError } from 'axios'
import { URL } from '@/configs/url'
import { Stage } from '@/shared/interfaces/stage'
import { FilterStage } from '@/shared/interfaces/filterStage'
import { getToken } from '@/utils/getToken'
import { loginService } from './login'

const URL_StageGetAll = `${URL}/api/track/process/`
const URL_IdStage = `${URL}/api/track/stage/`
const URL_GetStageAllByUserId = `${URL}/api/track/stage/get`
const URL_CountStage = `${URL}/api/track/stage/count`

export const stageService = {
	async getStageAllByProcessId(
		id: number | undefined,
		countRepeat = 0
	): Promise<Stage[] | null | undefined> {
		try {
			if (typeof id === 'number') {
				const data: Stage[] = await (
					await axios.get(`${URL_StageGetAll}${id}/stage`, {
						headers: {
							authorization: `Bearer ${getToken().accessToken}`,
							Accept: 'application/json',
							'Content-Type': 'application/json',
						},
					})
				).data

				return data
			}

			return null
		} catch (error) {
			if (countRepeat < 2 && (error as AxiosError).response?.status === 401) {
				await loginService.refreshToken()
				return this.getStageAllByProcessId(id, countRepeat + 1)
			}

			if (error instanceof Error) {
				console.log(error)
			}
		}
	},
	async getStageId(
		openedStageID: number | undefined,
		countRepeat = 0
	): Promise<Stage | null | undefined> {
		try {
			if (typeof openedStageID === 'undefined') return null

			const data: Stage = await (
				await axios.get(`${URL_IdStage}${openedStageID}`, {
					headers: {
						authorization: `Bearer ${getToken().accessToken}`,
						Accept: 'application/json',
						'Content-Type': 'application/json',
					},
				})
			).data

			return data
		} catch (error) {
			if (countRepeat < 2 && (error as AxiosError).response?.status === 401) {
				await loginService.refreshToken()
				return this.getStageId(openedStageID, countRepeat + 1)
			}

			if (error instanceof Error) {
				console.log(error.message)
			}
		}
	},
	async getStageAllByUserId(
		filters: FilterStage,
		limit: number,
		offset: number,
		countRepeat = 0
	): Promise<Stage[] | undefined> {
		try {
			const data: Stage[] = await (
				await axios.post(URL_GetStageAllByUserId, filters, {
					headers: {
						authorization: `Bearer ${getToken().accessToken}`,
						Accept: 'application/json',
						'Content-Type': 'application/json',
					},
					params: {
						limit: limit,
						offset: offset - 1,
					},
				})
			).data

			return data
		} catch (error) {
			if (countRepeat < 2 && (error as AxiosError).response?.status === 401) {
				await loginService.refreshToken()
				return this.getStageAllByUserId(filters, limit, offset, countRepeat + 1)
			}

			if (error instanceof Error) {
				console.log(error)
			}
		}
	},
	async successStage(stageId: number | undefined, countRepeat = 0) {
		try {
			if (stageId) {
				await axios.get(`${URL_IdStage}${stageId}/assign`, {
					headers: {
						authorization: `Bearer ${getToken().accessToken}`,
						Accept: 'application/json',
						'Content-Type': 'application/json',
					},
				})
			}
		} catch (error) {
			if (countRepeat < 2 && (error as AxiosError).response?.status === 401) {
				await loginService.refreshToken()
				await this.successStage(stageId, countRepeat + 1)
			}

			if (error instanceof Error) {
				console.log(error.message)
			}
		}
	},
	async cancelStage(stageId: number | undefined, countRepeat = 0) {
		try {
			if (stageId) {
				await axios.get(`${URL_IdStage}${stageId}/cancel`, {
					headers: {
						authorization: `Bearer ${getToken().accessToken}`,
						Accept: 'application/json',
						'Content-Type': 'application/json',
					},
				})
			}
		} catch (error) {
			if (countRepeat < 2 && (error as AxiosError).response?.status === 401) {
				await loginService.refreshToken()
				await this.cancelStage(stageId, countRepeat + 1)
			}

			if (error instanceof Error) {
				console.log(error.message)
			}
		}
	},
	async toggleStagePass(stage: Stage | undefined | null, countRepeat = 0) {
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
					pass: stage.pass,
				}

				await axios.put(`${URL_IdStage}${stage.id}/update`, data, {
					headers: {
						authorization: `Bearer ${getToken().accessToken}`,
						Accept: 'application/json',
						'Content-Type': 'application/json',
					},
				})
			}
		} catch (error) {
			if (countRepeat < 2 && (error as AxiosError).response?.status === 401) {
				await loginService.refreshToken()
				await this.toggleStagePass(stage, countRepeat + 1)
			}

			if (error instanceof Error) {
				console.log(error.message)
			}
		}
	},
	async getCountStage(
		filters: FilterStage,
		countRepeat = 0
	): Promise<number | undefined> {
		try {
			const countStage: number = await (
				await axios.post(URL_CountStage, filters, {
					headers: {
						authorization: `Bearer ${getToken().accessToken}`,
						Accept: 'application/json',
						'Content-Type': 'application/json',
					},
				})
			).data

			return countStage
		} catch (error) {
			if (countRepeat < 2 && (error as AxiosError).response?.status === 401) {
				await loginService.refreshToken()
				return this.getCountStage(filters, countRepeat + 1)
			}

			if (error instanceof Error) {
				console.log(error)
			}
		}
	},
}
