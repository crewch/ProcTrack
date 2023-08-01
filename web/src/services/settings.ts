import axios from 'axios'
import { URL } from '../configs/url'
import { processService } from './process'

const URL_processStatuses = `${URL}/api/track/property/processStatuses`
const URL_types = `${URL}/api/track/property/types`

export const settingsService = {
	async getSettingsProcess() {
		try {
			const statuses: string[] = await (
				await axios.get(URL_processStatuses)
			).data

			const types: string[] = await (await axios.get(URL_types)).data

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
