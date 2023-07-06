import { Box, Typography } from '@mui/material'
import TextTegs from './TextTags/TextTags'
import Pen from '/pen.svg'
import { FC } from 'react'
import styles from '/src/styles/MainPageStyles/SelectedProcessStyles/InfoProcessStyles/HeaderProcessFieldStyles/HeaderProcessField.module.scss'

const HeaderField: FC<{
	name: string
	status: string
	importance: string
	type: string
}> = ({ name, status, importance, type }) => {
	return (
		<>
			<Box className={styles.header}>
				<Box className={styles.wrap}>
					<Typography variant='h4' className={styles.typography}>
						{`${name} `}
						{status === 'в процессе' && (
							<img src='/inprogress.svg' className={styles.img} />
						)}
						{status === 'отменен' && (
							<img src='/rejected.svg' className={styles.img} />
						)}
						{status === 'завершен' && (
							<img src='/completed.svg' className={styles.img} />
						)}
						{status === 'остановлен' && (
							<img src='/stoppedProcess.svg' className={styles.img} />
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
			<TextTegs importance={importance} status={status} type={type} />
		</>
	)
}

export default HeaderField
