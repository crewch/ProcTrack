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
						{status === 'Согласовано' && (
							<img src='/completed.svg' loading='lazy' className={styles.img} />
						)}
						{status === 'Не начат' && (
							<img
								src='/stoppedProcess.svg'
								loading='lazy'
								className={styles.img}
							/>
						)}
						{status === 'Согласовано-Блокировано' && (
							<img src='/lock.svg' loading='lazy' className={styles.img} />
						)}
						{status === 'Принят на проверку' && (
							<img
								src='/arrow-circle-down.svg'
								loading='lazy'
								className={styles.img}
							/>
						)}
						{status === 'Отправлен на проверку' && (
							<img
								src='/arrow-circle-right.svg'
								loading='lazy'
								className={styles.img}
							/>
						)}
						{status === 'Отменен' && (
							<img src='/rejected.svg' loading='lazy' className={styles.img} />
						)}
						{status === 'Остановлен' && (
							<img
								src='/pause-circle.svg'
								loading='lazy'
								className={styles.img}
							/>
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
