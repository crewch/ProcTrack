import axios from 'axios'
import { URL } from '@/configs/url'
import { processService } from './process'

const URL_processStatuses = `${URL}/api/track/property/processStatuses`
const URL_processTypes = `${URL}/api/track/property/types`
const URL_stageStatuses = `${URL}/api/track/property/stageStatuses`

export const filtersService = {
	async getFiltersProcess() {
		try {
			const statuses: string[] = await (
				await axios.get(URL_processStatuses)
			).data

			const types: string[] = await (await axios.get(URL_processTypes)).data

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
			const statuses: string[] = await (await axios.get(URL_stageStatuses)).data

			const types: string[] = await (await axios.get(URL_processTypes)).data

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
