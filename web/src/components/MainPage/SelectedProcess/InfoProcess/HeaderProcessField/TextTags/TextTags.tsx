import { Box } from '@mui/material'
import TextTag from './TextTag/TextTag'
import { FC } from 'react'
import { ITextTagsProps } from '../../../../../../interfaces/IMainPage/ISelectedProcess/IInfoProcess/ITextTags/ITextTagsProps'
import styles from '/src/styles/MainPageStyles/SelectedProcessStyles/InfoProcessStyles/HeaderProcessFieldStyles/TextTagsStyles/TextTags.module.scss'

const TextTags: FC<ITextTagsProps> = ({ importance, status, type }) => {
	return (
		<Box className={styles.textTags}>
			{status === 'в процессе' && (
				<TextTag
					text={status}
					color='#89641B'
					backgroundColor='rgba(235, 184, 85, 0.50)'
					borderColor='#EBB855'
				/>
			)}
			{status === 'отменен' && (
				<TextTag
					text={status}
					color='#981418'
					backgroundColor='rgba(226, 94, 99, 0.50)'
					borderColor='#E25E63'
				/>
			)}
			{status === 'завершен' && (
				<TextTag
					text={status}
					color='#19712D'
					backgroundColor='rgba(84, 193, 108, 0.50)'
					borderColor='#54C16C'
				/>
			)}
			{status === 'остановлен' && (
				<TextTag
					text={status}
					color='#814422'
					backgroundColor='rgba(238, 140, 85, 0.50)'
					borderColor='#EE8C55'
				/>
			)}
			<TextTag
				text={type}
				color='#333333'
				backgroundColor='#ECECEC'
				borderColor='#ECECEC'
			/>
			<TextTag
				text={importance}
				color='#333333'
				backgroundColor='#ECECEC'
				borderColor='#ECECEC'
			/>
		</Box>
	)
}

export default TextTags
