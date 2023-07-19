import { Box, Typography } from '@mui/material'
import TextTags from './TextTags/TextTags'
import Pen from '/pen.svg'
import { FC, memo } from 'react'
import HeaderFieldImg from './HeaderFieldImg/HeaderFieldImg'
import styles from '/src/styles/MainPageStyles/SelectedProcessStyles/InfoProcessStyles/HeaderProcessFieldStyles/HeaderProcessField.module.scss'

interface HeaderFieldProps {
	name: string
	status: string
	importance: string
	type: string
	page?: 'main' | 'stageForSuccess'
}

const HeaderField: FC<HeaderFieldProps> = memo(
	({ name, status, importance, type, page }) => {
		return (
			<>
				<Box className={styles.header}>
					<Box className={styles.wrap}>
						<Typography variant='h4' className={styles.typography}>
							{name}
							<HeaderFieldImg status={status} />
						</Typography>
						{page === 'main' && (
							<Box className={styles.icon}>
								<img src={Pen} height='25px' width='25px' />
							</Box>
						)}
					</Box>
				</Box>
				<TextTags importance={importance} status={status} type={type} />
			</>
		)
	}
)

export default HeaderField
