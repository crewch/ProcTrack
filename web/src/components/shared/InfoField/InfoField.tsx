import { Box, Typography } from '@mui/material'
import { FC, memo } from 'react'
import InfoFieldImg from './InfoFieldImg/InfoFieldImg'
import Tags from '../Tags/Tags'
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
						<Typography variant='h4' className={styles.typography}>
							{name}
							<InfoFieldImg status={status} />
						</Typography>
						{page === 'release' && (
							<Box className={styles.icon}>
								<img src='/pen.svg' height='25px' width='25px' />
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
