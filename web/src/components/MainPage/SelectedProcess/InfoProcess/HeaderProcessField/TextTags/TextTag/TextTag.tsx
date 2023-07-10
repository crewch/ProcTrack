import { Typography } from '@mui/material'
import { FC } from 'react'
import { ITextTagProps } from '../../../../../../../interfaces/IMainPage/ISelectedProcess/IInfoProcess/ITextTags/ITextTag/ITextTagProps'

const TextTag: FC<ITextTagProps> = ({ text, color, backgroundColor }) => {
	return (
		<Typography
			sx={{
				backgroundColor: backgroundColor,
				py: '1px',
				px: { xs: '2px', lg: 1 },
				borderRadius: '3px',
				fontSize: { xs: '12px', xl: '16px' },
				color: color,
			}}
			variant='body2'
		>
			{text}
		</Typography>
	)
}

export default TextTag
