import axios from 'axios'
import fileDownload from 'js-file-download'

const URL_sendFile = 'http://localhost:8002/upload'
const URL_getFile = 'http://localhost:8002/download'

export const fileApi = {
	async sendFile(file: FormData) {
		try {
			const data = await (await axios.post(URL_sendFile, file)).data

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
