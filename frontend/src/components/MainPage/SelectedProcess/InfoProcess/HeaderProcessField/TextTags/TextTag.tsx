import { Typography } from '@mui/material'
import { FC } from 'react'
import { ITextTagProps } from '../../../../../../interfaces/IMainPage/ISelectedProcess/IInfoProcess/ITextTags/ITextTag/ITextTagProps'

const TextTag: FC<ITextTagProps> = ({ text, color, backgroundColor }) => {
	return (
		<Typography
			sx={{
				backgroundColor: backgroundColor,
				py: '1px',
				px: 0.5,
				borderRadius: '3px',
				fontSize: '12px',
				color: color,
			}}
			variant='body2'
		>
			{text}
		</Typography>
	)
}

export default TextTag
