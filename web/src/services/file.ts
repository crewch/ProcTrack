import axios from 'axios'
import fileDownload from 'js-file-download'
import { HOST } from '@/configs/url'
import { getToken } from '@/utils/getToken'

const URL_sendFile = `http://${HOST}:8002/upload`
const URL_getFile = `http://${HOST}:8002/download`

export const fileService = {
	async sendFile(file: FormData) {
		try {
			const data = await (
				await axios.post(URL_sendFile, file, {
					headers: {
						authorization: `Bearer ${getToken().accessToken}`,
						Accept: 'application/json',
						'Content-Type': 'application/json',
					},
				})
			).data

			return data.fileName
		} catch (error) {
			if (error instanceof Error) {
				console.log(error.message)
			}
		}
	},
	async getFile(fileRef: string) {
		try {
			const file = await (
				await axios.get(URL_getFile, {
					params: { fileName: fileRef },
					responseType: 'blob',

					headers: {
						authorization: `Bearer ${getToken().accessToken}`,
						Accept: 'application/json',
						'Content-Type': 'application/json',
					},
				})
			).data

			await fileDownload(file, fileRef)
		} catch (error) {
			if (error instanceof Error) {
				console.log(error.message)
			}
		}
	},
}
