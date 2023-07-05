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
			{status === 'отменён' && (
				<TextTag text={status} color='#540E10' backgroundColor='#E25E63' />
			)}
			{status === 'заврешено' && (
				<TextTag text={status} color='#3E5107' backgroundColor={'#AAC954'} />
			)}
			<TextTag text={type} color='#black' backgroundColor={'#ECECEC'} />
			<TextTag text={importance} color='#black' backgroundColor={'#ECECEC'} />
		</Box>
	)
}

export default TextTags
