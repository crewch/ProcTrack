import { Box, Typography } from '@mui/material'
import TextTegs from './TextTags/TextTags'
import Pen from '/src/assets/pen.svg'
import { FC } from 'react'
import styles from '/src/styles/MainPageStyles/SelectedProcessStyles/InfoProcessStyles/HeaderProcessFieldStyles/HeaderProcessField.module.scss'

const HeaderProcessField: FC<{
	nameOfProcess: string
	statusOfProcess: string
	importanceOfProcess: string
	typeOfProcess: string
}> = ({
	nameOfProcess,
	statusOfProcess,
	importanceOfProcess,
	typeOfProcess,
}) => {
	const ProcessStatusImg =
		statusOfProcess === 'в процессе' ? (
			<img src='src/assets/inprogress.svg' className={styles.img} />
		) : statusOfProcess === 'отклонено' ? (
			<img src='src/assets/rejected.svg' className={styles.img} />
		) : (
			<img src='src/assets/completed.svg' className={styles.img} />
		)

	return (
		<>
			<Box className={styles.header}>
				<Box className={styles.wrap}>
					<Typography variant='h4' className={styles.typography}>
						{`${nameOfProcess} `}
						{ProcessStatusImg}
					</Typography>
					<Box className={styles.icon}>
						<img src={Pen} />
					</Box>
				</Box>
			</Box>
			<TextTegs
				importance={importanceOfProcess}
				status={statusOfProcess}
				type={typeOfProcess}
			/>
		</>
	)
}

export default HeaderProcessField
