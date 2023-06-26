import { Box, Typography } from '@mui/material'
import TextTegs from './TextTags/TextTags'
import Pen from '/src/assets/pen.svg'
import { FC } from 'react'

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
			<img src='src/assets/inprogress.svg' />
		) : statusOfProcess === 'отклонено' ? (
			<img src='src/assets/rejected.svg' />
		) : (
			<img src='src/assets/completed.svg' />
		)

	return (
		<>
			<Box
				sx={{
					display: 'flex',
					flexDirection: 'column',
				}}
			>
				<Box
					sx={{ display: 'flex', justifyContent: 'space-between', mb: '2px' }}
				>
					<Typography
						variant='h4'
						sx={{
							fontFamily: 'Montserrat',
							fontWeight: 600,
							fontSize: '32px',
							color: '#1A2D67',
						}}
					>
						{`${nameOfProcess} `}
						{ProcessStatusImg}
					</Typography>
					<Box
						sx={{
							display: 'flex',
							justifyContent: 'end',
							alignItems: 'center',
						}}
					>
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
