import axios from 'axios'
import { IPassport } from '../interfaces/IApi/IPassportApi'
import { fileApi } from './fileApi'
import { URL } from './URL/URL'

const URL_getFileName = `http://${URL}/api/track/process/`
const URL_sendFile = `http://${URL}/api/track/process/`

export const passportApi = {
	async getPassport(processId: number) {
		try {
			const fileRef: IPassport[] = await (
				await axios.get(`${URL_getFileName}${processId}/passport`)
			).data

			if (fileRef) {
				return fileRef.sort((a, b) => b.id - a.id)
			}

			return null
		} catch (error) {
			if (error instanceof Error) {
				console.log(error.message)
			}
		}
	},
	async sendFilePassport(
		processId: number,
		file: FormData | undefined,
		message: string
	) {
		if (file) {
			const fileRef = await fileApi.sendFile(file)

			const userDataText = localStorage.getItem('UserData')

			if (userDataText) {
				await axios.post(
					`${URL_sendFile}${processId}/passport`,
					{
						title: fileRef,
						message,
					},
					{
						params: {
							userId: JSON.parse(userDataText).id,
						},
					}
				)
			}
		}
	},
}
