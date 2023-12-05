import { Box, Divider, Tooltip, Typography } from '@mui/material'
import { FC, memo } from 'react'
import InfoFieldImg from './InfoFieldImg/InfoFieldImg'
import Tags from '@/components/ui/Tags/Tags'
import styles from './InfoField.module.scss'

interface InfoFieldProps {
	name: string
	status: string
	importance?: string
	type?: string
	nameOfGroup?: string
	page: 'release' | 'approval'
	description?: string
}

const InfoField: FC<InfoFieldProps> = memo(
	({ name, status, importance, type, nameOfGroup, page, description }) => {
		const maxLengthTitle = page === 'release' ? 20 : 40

		return (
			<>
				<Box className={styles.header}>
					<Box className={styles.wrap}>
						{name?.length > maxLengthTitle ? (
							<Tooltip title={name} arrow>
								<Typography variant='h4' className={styles.typography}>
									{name.slice(0, maxLengthTitle)}...
									<InfoFieldImg status={status} />
								</Typography>
							</Tooltip>
						) : (
							<Typography variant='h4' className={styles.typography}>
								{name}
								<InfoFieldImg status={status} />
							</Typography>
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
				{description && description?.length && (
					<>
						<Divider sx={{ borderWidth: '0.025rem', my: '0.225rem' }} />
						<Box className={styles.wrapDescription}>
							<Typography variant='h6' className={styles.description}>
								Описание процесса:
							</Typography>
							{description.length > 120 ? (
								<Tooltip title={description} arrow>
									<Typography className={styles.description}>
										{description.slice(0, 120)}...
									</Typography>
								</Tooltip>
							) : (
								<Typography>{description}</Typography>
							)}
						</Box>
					</>
				)}
			</>
		)
	}
)

export default InfoField
