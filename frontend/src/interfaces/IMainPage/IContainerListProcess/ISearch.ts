import { Dispatch } from 'react'

export interface ISearchProps {
	textForSearchProcess: string
	setTextForSearchProcess: Dispatch<React.SetStateAction<string>>
}
