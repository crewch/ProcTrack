import { Box } from '@mui/material'
import TextTag from './TextTag/TextTag'
import { FC } from 'react'
import { ITextTagsProps } from '../../../../../../interfaces/IMainPage/ISelectedProcess/IInfoProcess/ITextTags/ITextTagsProps'
import styles from '/src/styles/MainPageStyles/SelectedProcessStyles/InfoProcessStyles/HeaderProcessFieldStyles/TextTagsStyles/TextTags.module.scss'

const TextTags: FC<ITextTagsProps> = ({ importance, status, type }) => {
	return (
		<Box className={styles.textTags}>
			{status === 'в процессе' && (
				<TextTag text={status} color='#5A3F0B' backgroundColor='#EBB855' />
			)}
			{status === 'отменен' && (
				<TextTag text={status} color='#540E10' backgroundColor='#E25E63' />
			)}
			{status === 'завершен' && (
				<TextTag text={status} color='#3E5107' backgroundColor='#AAC954' />
			)}
			{status === 'остановлен' && (
				<TextTag text={status} color='#3E5107' backgroundColor='#ECECEC' />
			)}
			<TextTag text={type} color='#3E5107' backgroundColor='#ECECEC' />
			<TextTag text={importance} color='#3E5107' backgroundColor='#ECECEC' />
		</Box>
	)
}

export default TextTags
