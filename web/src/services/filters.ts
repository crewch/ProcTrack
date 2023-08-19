import axios, { AxiosError } from 'axios'
import { URL } from '@/configs/url'
import { processService } from './process'
import { getToken } from '@/utils/getToken'
import { loginService } from './login'

const URL_processStatuses = `${URL}/api/track/property/processStatuses`
const URL_processTypes = `${URL}/api/track/property/types`
const URL_stageStatuses = `${URL}/api/track/property/stageStatuses`

export const filtersService = {
	async getFiltersProcess(
		countRepeat = 0
	): Promise<
		{ statuses: string[]; types: string[]; priorities: string[] } | undefined
	> {
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
			if (countRepeat < 2 && (error as AxiosError).response?.status === 401) {
				await loginService.refreshToken()
				return this.getFiltersProcess(countRepeat + 1)
			}

			if (error instanceof Error) {
				console.log(error)
			}
		}
	},
	async getFiltersStage(
		countRepeat = 0
	): Promise<
		{ statuses: string[]; types: string[]; priorities: string[] } | undefined
	> {
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
			if (countRepeat < 2 && (error as AxiosError).response?.status === 401) {
				await loginService.refreshToken()
				return this.getFiltersStage(countRepeat + 1)
			}

			if (error instanceof Error) {
				console.log(error)
			}
		}
	},
}
