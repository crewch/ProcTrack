import { Box, Divider, Tooltip, Typography } from '@mui/material'
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
	description?: string
}

const InfoField: FC<InfoFieldProps> = memo(
	({ name, status, importance, type, nameOfGroup, page, description }) => {
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
				{description && (
					<>
						<Divider sx={{ borderWidth: '0.025rem', my: '0.225rem' }} />
						{description.length > 60 ? (
							<>
								<Tooltip title={description} arrow>
									<Typography>{description.slice(0, 59)}...</Typography>
								</Tooltip>
							</>
						) : (
							<>
								<Typography>{description}</Typography>
							</>
						)}
					</>
				)}
			</>
		)
	}
)

export default InfoField
