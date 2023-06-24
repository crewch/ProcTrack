import { ListItem, ListItemIcon } from '@mui/material'
import { FC } from 'react'
import { ILogoListItemProps } from '../../interfaces/ILayout/ILogoListItem'

const LogoListItem: FC<ILogoListItemProps> = ({ src }) => {
	return (
		<ListItem sx={{ justifyContent: 'center' }}>
			<ListItemIcon sx={{ justifyContent: 'center' }}>
				<img src={src} height='50px' width='49px' />
			</ListItemIcon>
		</ListItem>
	)
}

export default LogoListItem
