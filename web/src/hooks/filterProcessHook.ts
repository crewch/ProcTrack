import { useMemo } from 'react'
import { IProcess } from '../interfaces/IApi/IApi'

export const useFilterProcess = (
	isSuccess: boolean,
	allProcess: IProcess[] | undefined,
	textForSearchProcess: string,
	settingsForSearch: string[]
) => {
	return useMemo(() => {
		if (isSuccess && allProcess) {
			return allProcess
				.sort((a, b) => b.id - a.id)
				.filter(process =>
					process.title
						.toLocaleLowerCase()
						.includes(textForSearchProcess.toLocaleLowerCase())
				)
				.filter(item => {
					if (!settingsForSearch.length) return item

					if (
						settingsForSearch.includes(item.priority) ||
						settingsForSearch.includes(item.type) ||
						settingsForSearch.includes(item.status)
					) {
						return item
					}
				})
		}

		return []
	}, [allProcess, isSuccess, settingsForSearch, textForSearchProcess])
}
