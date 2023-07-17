import { Box } from '@mui/material'
import { FC } from 'react'
import TextTag from '../../../SelectedProcess/InfoProcess/HeaderProcessField/TextTags/TextTag/TextTag'
import { ITextTagsProps } from '../../../../../interfaces/IMainPage/ISelectedStage/IHeaderField/ITextTags/ITextTags'
import styles from '/src/styles/MainPageStyles/SelectedProcessStyles/InfoProcessStyles/HeaderProcessFieldStyles/TextTagsStyles/TextTags.module.scss'

const TextTags: FC<ITextTagsProps> = ({ status, nameOfGroup }) => {
	return (
		<Box className={styles.textTags}>
			{status === 'Согласовано' && (
				<TextTag
					text={status}
					color='#19712D'
					backgroundColor='rgba(84, 193, 108, 0.50)'
					borderColor='#54C16C'
				/>
			)}
			{status === 'Не начат' && (
				<TextTag
					text={status}
					color='#5A3F0B'
					backgroundColor='#EBB855'
					borderColor='#EBB855'
				/>
			)}
			{status === 'Согласовано-Блокировано' && (
				<TextTag
					text={status}
					color='#13594D'
					backgroundColor='rgba(37, 200, 170, 0.50)'
					borderColor='#25C8AA'
				/>
			)}
			{status === 'Принят на проверку' && (
				<TextTag
					text={status}
					color='#134A7C'
					backgroundColor='rgba(92, 152, 208, 0.50)'
					borderColor='#5C98D0'
				/>
			)}
			{status === 'Отправлен на проверку' && (
				<TextTag
					text={status}
					color='#145968'
					backgroundColor='rgba(93, 199, 222, 0.50)'
					borderColor='#5DC7DE'
				/>
			)}
			{status === 'Отменен' && (
				<TextTag
					text={status}
					color='#981418'
					backgroundColor='rgba(226, 94, 99, 0.50)'
					borderColor='#E25E63'
				/>
			)}
			{status === 'Остановлен' && (
				<TextTag
					text={status}
					color='#814422'
					backgroundColor='rgba(238, 140, 85, 0.50)'
					borderColor='#EE8C55'
				/>
			)}
			<TextTag
				text={nameOfGroup}
				color='#black'
				backgroundColor='#ECECEC'
				borderColor='#ECECEC'
			/>
		</Box>
	)
}

export default TextTags
