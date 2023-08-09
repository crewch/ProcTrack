import { Box } from '@mui/material'
import { FC, memo } from 'react'
import Tag from './Tag/Tag'
import styles from './Tags.module.scss'

interface TagsProps {
	status: string
	nameOfGroup?: string
	type?: string
	importance?: string
}

const Tags: FC<TagsProps> = memo(
	({ status, nameOfGroup, importance, type }) => {
		return (
			<Box className={styles.tags}>
				{status === 'Согласовано' && (
					<Tag
						text={status}
						color='#19712D'
						backgroundColor='rgba(84, 193, 108, 0.50)'
						borderColor='#54C16C'
					/>
				)}
				{status === 'Не начат' && (
					<Tag
						text={status}
						color='#5A3F0B'
						backgroundColor='#EBB855'
						borderColor='#EBB855'
					/>
				)}
				{status === 'Согласовано-Блокировано' && (
					<Tag
						text={status}
						color='#13594D'
						backgroundColor='rgba(37, 200, 170, 0.50)'
						borderColor='#25C8AA'
					/>
				)}
				{status === 'Принят на проверку' && (
					<Tag
						text={status}
						color='#134A7C'
						backgroundColor='rgba(92, 152, 208, 0.50)'
						borderColor='#5C98D0'
					/>
				)}
				{status === 'Отправлен на проверку' && (
					<Tag
						text={status}
						color='#145968'
						backgroundColor='rgba(93, 199, 222, 0.50)'
						borderColor='#5DC7DE'
					/>
				)}
				{status === 'Отменен' && (
					<Tag
						text={status}
						color='#981418'
						backgroundColor='rgba(226, 94, 99, 0.50)'
						borderColor='#E25E63'
					/>
				)}
				{status === 'Остановлен' && (
					<Tag
						text={status}
						color='#814422'
						backgroundColor='rgba(238, 140, 85, 0.50)'
						borderColor='#EE8C55'
					/>
				)}
				{nameOfGroup && (
					<Tag
						text={nameOfGroup}
						color='#333'
						backgroundColor='#ECECEC'
						borderColor='#ECECEC'
					/>
				)}
				{status === 'в процессе' && (
					<Tag
						text={status}
						color='#89641B'
						backgroundColor='rgba(235, 184, 85, 0.50)'
						borderColor='#EBB855'
					/>
				)}
				{status === 'отменен' && (
					<Tag
						text={status}
						color='#981418'
						backgroundColor='rgba(226, 94, 99, 0.50)'
						borderColor='#E25E63'
					/>
				)}
				{status === 'завершен' && (
					<Tag
						text={status}
						color='#19712D'
						backgroundColor='rgba(84, 193, 108, 0.50)'
						borderColor='#54C16C'
					/>
				)}
				{status === 'остановлен' && (
					<Tag
						text={status}
						color='#814422'
						backgroundColor='rgba(238, 140, 85, 0.50)'
						borderColor='#EE8C55'
					/>
				)}
				{type && (
					<Tag
						text={type}
						color='#333333'
						backgroundColor='#ECECEC'
						borderColor='#ECECEC'
					/>
				)}
				{importance && (
					<Tag
						text={importance}
						color='#333333'
						backgroundColor='#ECECEC'
						borderColor='#ECECEC'
					/>
				)}
			</Box>
		)
	}
)

export default Tags
