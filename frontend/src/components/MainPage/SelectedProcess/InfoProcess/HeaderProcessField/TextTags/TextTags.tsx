import { Box } from '@mui/material'
import TextTeg from './TextTag'
import { FC } from 'react'
import { ITextTagsProps } from '../../../../../../interfaces/IMainPage/ISelectedProcess/IInfoProcess/ITextTags/ITextTagsProps'
import styles from '/src/styles/MainPageStyles/SelectedProcessStyles/InfoProcessStyles/HeaderProcessFieldStyles/TextTagsStyles/TextTags.module.scss'

const TextTags: FC<ITextTagsProps> = ({ importance, status, type }) => {
	return (
		<Box className={styles.textTags}>
			{status === 'в процессе' && (
				<TextTeg text={status} color='#5A3F0B' backgroundColor='#EBB855' />
			)}
			{status === 'заврешено' && (
				<TextTeg text={status} color='#ECECEC' backgroundColor={'#98FB98'} />
			)}
			<TextTeg text={type} color='#black' backgroundColor={'#ECECEC'} />
			<TextTeg text={importance} color='#black' backgroundColor={'#ECECEC'} />
		</Box>
	)
}

export default TextTags
