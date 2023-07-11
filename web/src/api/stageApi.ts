import axios from 'axios'
import { IUser } from '../interfaces/IApi/IApi'
import { IStage } from '../interfaces/IApi/IGetStageApi'

const URL_Stage = 'http://localhost:8000/api/track/stage/'

export const stageApi = {
	async successStage(stageId: number | undefined) {
		try {
			const textUserData = localStorage.getItem('UserData')

			if (textUserData && stageId) {
				const userData: IUser = JSON.parse(textUserData)

				await axios.get(`${URL_Stage}${stageId}/assign`, {
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

				await axios.get(`${URL_Stage}${stageId}/cancel`, {
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
	async toggleStagePass(stage: IStage) {
		try {
			const textUserData = localStorage.getItem('UserData')

			if (textUserData && stage) {
				const userData: IUser = JSON.parse(textUserData)

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

				await axios.put(`${URL_Stage}${stage.id}/update`, data, {
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
