import { Box, Tooltip, Typography } from '@mui/material'
import { FC, memo } from 'react'
import InfoFieldImg from './InfoFieldImg/InfoFieldImg'
import Tags from '../../ui/Tags/Tags'
import styles from './InfoField.module.scss'

interface InfoFieldProps {
	name: string
	status: string
	importance?: string
	type?: string
	nameOfGroup?: string
	page: 'release' | 'approval'
}

const InfoField: FC<InfoFieldProps> = memo(
	({ name, status, importance, type, nameOfGroup, page }) => {
		return (
			<>
				<Box className={styles.header}>
					<Box className={styles.wrap}>
						{name.length < 30 ? (
							<Typography variant='h4' className={styles.typography}>
								{name}
								<InfoFieldImg status={status} />
							</Typography>
						) : (
							<Tooltip title={name} arrow>
								<Typography variant='h4' className={styles.typography}>
									{name.slice(0, 31)}...
									<InfoFieldImg status={status} />
								</Typography>
							</Tooltip>
						)}
						{page === 'release' && (
							<Box className={styles.icon}>
								<img src='/pen.svg' className={styles.iconImg} />
							</Box>
						)}
					</Box>
				</Box>
				<Tags
					importance={importance}
					status={status}
					type={type}
					nameOfGroup={nameOfGroup}
				/>
			</>
		)
	}
)

export default InfoField
