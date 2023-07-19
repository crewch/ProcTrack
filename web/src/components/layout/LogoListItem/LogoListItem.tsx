import { ListItem, ListItemIcon } from '@mui/material'
import { FC } from 'react'
import styles from './LogoListItem.module.scss'

interface LogoListItemProps {
	pathImg: string
}

const LogoListItem: FC<LogoListItemProps> = ({ pathImg }) => {
	return (
		<ListItem disablePadding className={styles.listItem}>
			<ListItemIcon className={styles.listItemIcon}>
				<img src={pathImg} className={styles.logo} />
			</ListItemIcon>
		</ListItem>
	)
}

export default LogoListItem
