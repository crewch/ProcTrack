import { Typography } from '@mui/material'
import { FC, memo } from 'react'

interface TextTagProps {
	text: string
	color: string
	backgroundColor: string
	borderColor: string
}

const TextTag: FC<TextTagProps> = memo(
	({ text, color, backgroundColor, borderColor }) => {
		return (
			<Typography
				sx={{
					color: color,
					backgroundColor: backgroundColor,
					borderColor: borderColor,
					py: '1px',
					px: { xs: '2px', lg: 1 },
					borderWidth: '1px',
					borderRadius: '3px',
					fontSize: { xs: '12px', xl: '16px' },
				}}
				variant='body2'
			>
				{text}
			</Typography>
		)
	}
)

export default TextTag
