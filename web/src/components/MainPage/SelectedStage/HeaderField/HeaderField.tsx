import { Box, Typography } from '@mui/material'
import Pen from '/pen.svg'
import { FC, memo } from 'react'
import styles from '/src/styles/MainPageStyles/SelectedProcessStyles/InfoProcessStyles/HeaderProcessFieldStyles/HeaderProcessField.module.scss'
import TextTags from './TextTags/TextTags'
import { IHeaderFieldProps } from '../../../../interfaces/IMainPage/ISelectedStage/IHeaderField/IHeaderField'
import HeaderFieldImg from '../../SelectedProcess/InfoProcess/HeaderProcessField/HeaderFieldImg/HeaderFieldImg'

const HeaderField: FC<IHeaderFieldProps> = memo(
	({ name, status, nameOfGroup, page }) => {
		return (
			<>
				<Box className={styles.header}>
					<Box className={styles.wrap}>
						<Typography variant='h4' className={styles.typography}>
							{`${name} `}
							<HeaderFieldImg status={status} />
						</Typography>
						{page === 'main' && (
							<Box className={styles.icon}>
								<img src={Pen} height='25px' width='25px' />
							</Box>
						)}
					</Box>
				</Box>
				<TextTags status={status} nameOfGroup={nameOfGroup} />
			</>
		)
	}
)

export default HeaderField
