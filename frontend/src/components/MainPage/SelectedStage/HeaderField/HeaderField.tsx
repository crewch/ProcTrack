import { Box, Typography } from '@mui/material'
import Pen from '/src/assets/pen.svg'
import { FC } from 'react'
import styles from '/src/styles/MainPageStyles/SelectedProcessStyles/InfoProcessStyles/HeaderProcessFieldStyles/HeaderProcessField.module.scss'
import TextTags from './TextTags/TextTags'

const HeaderField: FC<{
	name: string
	status: string
	nameOfGroup: string
}> = ({ name, status, nameOfGroup }) => {
	const ProcessStatusImg =
		status === 'в процессе' ? (
			<img src='src/assets/inprogress.svg' className={styles.img} />
		) : status === 'отклонено' ? (
			<img src='src/assets/rejected.svg' className={styles.img} />
		) : (
			status === 'согласован с замечаниями' && (
				<img src='src/assets/completed.svg' className={styles.img} />
			)
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
			<TextTags status={status} nameOfGroup={nameOfGroup} />
		</>
	)
}

export default HeaderField
