import { Typography } from '@mui/material'
import { FC, memo } from 'react'

interface TagProps {
	text: string
	color: string
	backgroundColor: string
	borderColor: string
}

const Tag: FC<TagProps> = memo(
	({ text, color, backgroundColor, borderColor }) => {
		return (
			<Typography
				sx={{
					color: color,
					backgroundColor: backgroundColor,
					borderColor: borderColor,
					py: '0,0625rem',
					px: { xs: '0,125rem', lg: '0.5rem' },
					borderWidth: '0.1rem',
					borderRadius: '0.2rem',
					fontSize: { xs: '0.75rem', xl: '1rem' },
				}}
				variant='body2'
			>
				{text}
			</Typography>
		)
	}
)

export default Tag
