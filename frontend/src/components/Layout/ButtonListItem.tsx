import { ListItem, ListItemButton, ListItemIcon, Tooltip } from '@mui/material'
import { FC } from 'react'
import { NavLink } from 'react-router-dom'
import { IButtonListItem } from '../../interfaces/ILayout/IButtonListItem'
import styles from '../../styles/LayoutStyles/ButtonListItemStyles/ButtonListItem.module.scss'

const ButtonListItem: FC<IButtonListItem> = ({ src, to }) => {
	return (
		<ListItem disablePadding className={styles.listItem}>
			<NavLink
				to={to}
				className={({ isActive }) =>
					isActive
						? `${styles.activeStyle} ${styles.navLink}`
						: `${styles.navLink}`
				}
			>
				<Tooltip title={to} placement='right' arrow>
					<ListItemButton className={styles.listItemButton}>
						<ListItemIcon
							className={`${styles.activeStyleButton} ${styles.listItemIcon}`}
						>
							<img src={src} className={styles.img} />
						</ListItemIcon>
					</ListItemButton>
				</Tooltip>
			</NavLink>
		</ListItem>
	)
}

export default ButtonListItem
