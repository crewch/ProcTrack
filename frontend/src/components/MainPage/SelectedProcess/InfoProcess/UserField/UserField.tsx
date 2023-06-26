import { Box, Typography } from '@mui/material'
import { FC } from 'react'

const UserField: FC<{ group: string; responsible: string }> = ({
	group,
	responsible,
}) => {
	return (
		<Box sx={{ display: 'flex', gap: 1 }}>
			<Box
				sx={{
					display: 'flex',
					justifyContent: 'center',
					alignItems: 'center',
				}}
			>
				<img height='38px' src='/src/assets/user1.svg' />
			</Box>
			<Box sx={{ display: 'flex', flexDirection: 'column' }}>
				<Typography
					variant='h6'
					sx={{
						fontFamily: 'Montserrat',
						fontWeight: '600',
						fontSize: '20px',
						color: '#333333',
					}}
				>
					Ответственный
				</Typography>
				<Box sx={{ display: 'flex', gap: 1 }}>
					<Typography
						variant='body1'
						sx={{
							fontSize: '14px',
							color: '#333333',
						}}
					>
						{responsible}
					</Typography>
					<Box
						sx={{
							display: 'flex',
							alignItems: 'center',
							backgroundColor: '#ECECEC',
							px: '2px',
							borderRadius: '3px',
						}}
					>
						<Typography
							variant='body2'
							sx={{
								fontSize: '12px',
								color: '#333333',
							}}
						>
							{group}
						</Typography>
					</Box>
				</Box>
			</Box>
		</Box>
	)
}

export default UserField
