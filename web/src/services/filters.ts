import axios from 'axios'
import { URL } from '@/configs/url'
import { processService } from './process'
import { getToken } from '@/utils/getToken'

const URL_processStatuses = `${URL}/api/track/property/processStatuses`
const URL_processTypes = `${URL}/api/track/property/types`
const URL_stageStatuses = `${URL}/api/track/property/stageStatuses`

export const filtersService = {
	async getFiltersProcess() {
		try {
			const statuses: string[] = await (
				await axios.get(URL_processStatuses, {
					headers: {
						authorization: `Bearer ${getToken().accessToken}`,
						Accept: 'application/json',
						'Content-Type': 'application/json',
					},
				})
			).data

			const types: string[] = await (
				await axios.get(URL_processTypes, {
					headers: {
						authorization: `Bearer ${getToken().accessToken}`,
						Accept: 'application/json',
						'Content-Type': 'application/json',
					},
				})
			).data

			const priorities: string[] | undefined =
				await processService.getPriorities()

			return {
				statuses,
				types,
				priorities: priorities !== undefined ? priorities : [],
			}
		} catch (error) {
			if (error instanceof Error) {
				console.log(error)
			}
		}
	},
	async getFiltersStage() {
		try {
			const statuses: string[] = await (
				await axios.get(URL_stageStatuses, {
					headers: {
						authorization: `Bearer ${getToken().accessToken}`,
						Accept: 'application/json',
						'Content-Type': 'application/json',
					},
				})
			).data

			const types: string[] = await (
				await axios.get(URL_processTypes, {
					headers: {
						authorization: `Bearer ${getToken().accessToken}`,
						Accept: 'application/json',
						'Content-Type': 'application/json',
					},
				})
			).data

			const priorities: string[] | undefined =
				await processService.getPriorities()

			return {
				statuses,
				types,
				priorities: priorities !== undefined ? priorities : [],
			}
		} catch (error) {
			if (error instanceof Error) {
				console.log(error)
			}
		}
	},
}
