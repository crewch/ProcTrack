import { Dispatch, SetStateAction } from 'react'

export interface ISearchProps {
	textForSearchProcess: string
	setTextForSearchProcess: Dispatch<SetStateAction<string>>
	isOpen: boolean
	setIsOpen: Dispatch<SetStateAction<boolean>>
}
