import { ListItem, ListItemButton, ListItemIcon, Tooltip } from '@mui/material'
import { FC } from 'react'
import { NavLink } from 'react-router-dom'
import { IButtonListItem } from '../../interfaces/ILayout/IButtonListItem'
import { useAppDispatch } from '../../hooks/reduxHooks'
import { reset } from '../../store/processSlice/processSlice'
import styles from '../../styles/LayoutStyles/ButtonListItemStyles/ButtonListItem.module.scss'

const ButtonListItem: FC<IButtonListItem> = ({ src, to, otherPage, label }) => {
	const dispatch = useAppDispatch()

	return (
		<ListItem disablePadding className={styles.listItem}>
			{otherPage ? (
				<a
					href='http://localhost:3000'
					target='_blank'
					className={styles.navLink}
				>
					<Tooltip title={label} placement='right' arrow>
						<ListItemButton
							className={styles.listItemButton}
							onClick={() => dispatch(reset())}
						>
							<ListItemIcon
								className={`${styles.activeStyleButton} ${styles.listItemIcon}`}
							>
								<img src={src} className={styles.img} />
							</ListItemIcon>
						</ListItemButton>
					</Tooltip>
				</a>
			) : (
				<NavLink
					to={to}
					className={({ isActive }) =>
						isActive
							? `${styles.activeStyle} ${styles.navLink}`
							: `${styles.navLink}`
					}
				>
					{({ isActive }) => (
						<Tooltip title={label} placement='right' arrow>
							<ListItemButton
								className={styles.listItemButton}
								onClick={() => dispatch(reset())}
							>
								<ListItemIcon
									className={`${styles.activeStyleButton} ${styles.listItemIcon}`}
								>
									{isActive ? (
										<img
											src={`${src.slice(0, -4)}Blue.svg`}
											className={styles.img}
										/>
									) : (
										<img src={src} className={styles.img} />
									)}
								</ListItemIcon>
							</ListItemButton>
						</Tooltip>
					)}
				</NavLink>
			)}
		</ListItem>
	)
}

export default ButtonListItem
