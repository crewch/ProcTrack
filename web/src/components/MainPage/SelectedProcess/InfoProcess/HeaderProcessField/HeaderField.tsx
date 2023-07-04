import { Box, Typography } from '@mui/material'
import TextTegs from './TextTags/TextTags'
import Pen from '/src/assets/pen.svg'
import { FC } from 'react'
import styles from '/src/styles/MainPageStyles/SelectedProcessStyles/InfoProcessStyles/HeaderProcessFieldStyles/HeaderProcessField.module.scss'

const HeaderField: FC<{
	name: string
	status: string
	importance: string
	type: string
}> = ({ name, status, importance, type }) => {
	const ProcessStatusImg =
		status === 'в процессе' ? (
			<img src='src/assets/inprogress.svg' className={styles.img} />
		) : status === 'отклонено' ? (
			<img src='src/assets/rejected.svg' className={styles.img} />
		) : (
			<img src='src/assets/completed.svg' className={styles.img} />
		)

	return (
		<>
			<Box className={styles.header}>
				<Box className={styles.wrap}>
					<Typography variant='h4' className={styles.typography}>
						{`${name} `}
						{ProcessStatusImg}
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
