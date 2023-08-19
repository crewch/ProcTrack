import axios, { AxiosError } from 'axios'
import { fileService } from './file'
import { URL } from '@/configs/url'
import { Passport } from '@/shared/interfaces/passport'
import { getToken } from '@/utils/getToken'
import { loginService } from './login'

const URL_getPassport = `${URL}/api/track/process/`
const URL_sendPassport = `${URL}/api/track/process/`

export const passportService = {
	async getPassport(
		processId: number,
		countRepeat = 0
	): Promise<Passport[] | undefined> {
		try {
			const fileRef: Passport[] = await (
				await axios.get(`${URL_getPassport}${processId}/passport`, {
					headers: {
						authorization: `Bearer ${getToken().accessToken}`,
						Accept: 'application/json',
						'Content-Type': 'application/json',
					},
				})
			).data

			if (fileRef) {
				return fileRef.sort((a, b) => b.id - a.id)
			}
		} catch (error) {
			if (countRepeat < 2 && (error as AxiosError).response?.status === 401) {
				await loginService.refreshToken()
				return this.getPassport(processId, countRepeat + 1)
			}

			if (error instanceof Error) {
				console.log(error.message)
			}
		}
	},
	async sendPassport(
		processId: number,
		file: FormData | undefined,
		message: string,
		countRepeat = 0
	) {
		try {
			if (file) {
				const title = await fileService.sendFile(file)

				await axios.post(
					`${URL_sendPassport}${processId}/passport`,
					{
						title,
						message,
					},
					{
						headers: {
							authorization: `Bearer ${getToken().accessToken}`,
							Accept: 'application/json',
							'Content-Type': 'application/json',
						},
					}
				)
			}
		} catch (error) {
			if (countRepeat < 2 && (error as AxiosError).response?.status === 401) {
				await loginService.refreshToken()
				await this.sendPassport(processId, file, message, countRepeat + 1)
			}

			if (error instanceof Error) {
				console.log(error.message)
			}
		}
	},
}
