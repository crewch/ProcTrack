import { ListItem, ListItemButton, ListItemIcon, Tooltip } from '@mui/material'
import { FC } from 'react'
import { NavLink } from 'react-router-dom'
import { IButtonListItem } from '../../interfaces/ILayout/IButtonListItem'
import styles from '../../styles/LayoutStyles/ButtonListItem.module.css'

const ButtonListItem: FC<IButtonListItem> = ({ src, to }) => {
	return (
		<ListItem disablePadding sx={{ mt: 1 }}>
			<NavLink
				to={to}
				style={{ width: '100%' }}
				className={({ isActive }) => (isActive ? styles.activeStyle : '')}
			>
				<Tooltip title={to} placement='right' arrow>
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
				</Tooltip>
			</NavLink>
		</ListItem>
	)
}

export default ButtonListItem
