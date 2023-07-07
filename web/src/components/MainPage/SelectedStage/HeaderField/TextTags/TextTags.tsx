import { Box } from '@mui/material'
import { FC } from 'react'
import TextTag from '../../../SelectedProcess/InfoProcess/HeaderProcessField/TextTags/TextTag/TextTag'
import styles from '/src/styles/MainPageStyles/SelectedProcessStyles/InfoProcessStyles/HeaderProcessFieldStyles/TextTagsStyles/TextTags.module.scss'
import { ITextTagsProps } from '../../../../../interfaces/IMainPage/ISelectedStage/IHeaderField/ITextTags/ITextTags'

const TextTags: FC<ITextTagsProps> = ({ status, nameOfGroup }) => {
	return (
		<Box className={styles.textTags}>
			{status === 'Согласовано' && (
				<TextTag text={status} color='#5A3F0B' backgroundColor='#EBB855' />
			)}
			{status === 'Не начат' && (
				<TextTag text={status} color='#5A3F0B' backgroundColor='#EBB855' />
			)}
			{status === 'Согласовано-Блокировано' && (
				<TextTag text={status} color='#5A3F0B' backgroundColor='#EBB855' />
			)}
			{status === 'Принят на проверку' && (
				<TextTag text={status} color='#5A3F0B' backgroundColor='#EBB855' />
			)}
			{status === 'Отправлен на проверку' && (
				<TextTag text={status} color='#5A3F0B' backgroundColor='#D8ECFF' />
			)}
			{status === 'Отменен' && (
				<TextTag text={status} color='#5A3F0B' backgroundColor='#EBB855' />
			)}
			{status === 'Остановлен' && (
				<TextTag text={status} color='#5A3F0B' backgroundColor='#EBB855' />
			)}
			<TextTag text={nameOfGroup} color='#black' backgroundColor={'#ECECEC'} />
		</Box>
	)
}

export default TextTags
