import { ListItem, ListItemButton, ListItemIcon, Tooltip } from '@mui/material'
import { FC } from 'react'
import { NavLink } from 'react-router-dom'
import { IButtonListItem } from '../../interfaces/ILayout/IButtonListItem'
import styles from '../../styles/LayoutStyles/ButtonListItem.module.css'

const ButtonListItem: FC<IButtonListItem> = ({ src, to }) => {
	return (
		<ListItem disablePadding sx={{ mt: 1 }}>
			<Tooltip title={to} placement='right' arrow>
				<NavLink
					to={to}
					style={{ width: '100%' }}
					className={({ isActive }) => (isActive ? styles.activeStyle : '')}
				>
					<ListItemButton sx={{ display: 'flex', justifyContent: 'center' }}>
						<ListItemIcon
							sx={{
								p: '5px',
							}}
							className={styles.activeStyleButton}
						>
							<img src={src} height='50px' width='49px' />
						</ListItemIcon>
					</ListItemButton>
				</NavLink>
			</Tooltip>
		</ListItem>
	)
}

export default ButtonListItem
