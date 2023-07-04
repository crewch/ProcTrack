import { Box, Typography } from '@mui/material'
import Pen from '/pen.svg'
import { FC } from 'react'
import styles from '/src/styles/MainPageStyles/SelectedProcessStyles/InfoProcessStyles/HeaderProcessFieldStyles/HeaderProcessField.module.scss'
import TextTags from './TextTags/TextTags'
import { IHeaderFieldProps } from '../../../../interfaces/IMainPage/ISelectedStage/IHeaderField/IHeaderField'

const HeaderField: FC<IHeaderFieldProps> = ({ name, status, nameOfGroup }) => {
	return (
		<>
			<Box className={styles.header}>
				<Box className={styles.wrap}>
					<Typography variant='h4' className={styles.typography}>
						{`${name} `}
						{status === 'в процессе' && (
							<img src='/inprogress.svg' className={styles.img} />
						)}
						{status === 'отклонено' && (
							<img src='/rejected.svg' className={styles.img} />
						)}
						{status === 'согласован с замечаниями' && (
							<img src='/completed.svg' className={styles.img} />
						)}
					</Typography>
					<Box className={styles.icon}>
						<img src={Pen} height='25px' width='25px' />
					</Box>
				</Box>
			</Box>
			<TextTags status={status} nameOfGroup={nameOfGroup} />
		</>
	)
}

export default HeaderField
