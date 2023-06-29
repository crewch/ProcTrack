import { ListItem, ListItemIcon } from '@mui/material'
import { FC } from 'react'
import { ILogoListItemProps } from '../../interfaces/ILayout/ILogoListItem'
import styles from '../../styles/LayoutStyles/LogoListItemStyles/LogoListItem.module.scss'

const LogoListItem: FC<ILogoListItemProps> = ({ src }) => {
	return (
		<ListItem disablePadding className={styles.listItem}>
			<ListItemIcon className={styles.listItemIcon}>
				<img src={src} className={styles.logo} />
			</ListItemIcon>
		</ListItem>
	)
}

export default LogoListItem
