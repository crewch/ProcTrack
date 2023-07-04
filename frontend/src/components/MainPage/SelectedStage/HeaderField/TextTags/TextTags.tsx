import { Box } from '@mui/material'
import { FC } from 'react'
import TextTag from '../../../SelectedProcess/InfoProcess/HeaderProcessField/TextTags/TextTag/TextTag'
import styles from '/src/styles/MainPageStyles/SelectedProcessStyles/InfoProcessStyles/HeaderProcessFieldStyles/TextTagsStyles/TextTags.module.scss'
import { ITextTagsProps } from '../../../../../interfaces/IMainPage/ISelectedStage/IHeaderField/ITextTags/ITextTags'

const TextTags: FC<ITextTagsProps> = ({ status, nameOfGroup }) => {
	return (
		<Box className={styles.textTags}>
			{status === 'в процессе' && (
				<TextTag text={status} color='#5A3F0B' backgroundColor='#EBB855' />
			)}
			{status === 'согласован с замечаниями' && (
				<TextTag text={status} color='#3E5107' backgroundColor='#AAC954' />
			)}
			{status === 'отменён' && (
				<TextTag text={status} color='#540E10' backgroundColor='#E25E63' />
			)}
			<TextTag text={nameOfGroup} color='#black' backgroundColor={'#ECECEC'} />
		</Box>
	)
}

export default TextTags
