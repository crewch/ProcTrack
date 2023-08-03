import { ListItem, ListItemButton, ListItemIcon, Tooltip } from '@mui/material'
import { FC } from 'react'
import { NavLink } from 'react-router-dom'
import { useAppDispatch } from '../../../hooks/reduxHooks'
import { reset } from '../../../store/processStageSlice/processStageSlice'
import styles from './ButtonListItem.module.scss'

interface ButtonListItemProps {
	label: string
	Img: React.FunctionComponent<
		React.SVGProps<SVGSVGElement> & {
			title?: string | undefined
		}
	>
	to: string
	otherPage?: string
}

const ButtonListItem: FC<ButtonListItemProps> = ({
	Img,
	to,
	otherPage,
	label,
}) => {
	const dispatch = useAppDispatch()

	return (
		<ListItem disablePadding className={styles.listItem}>
			{otherPage ? (
				<a href={otherPage} target='_blank' className={styles.navLink}>
					<Tooltip title={label} placement='right' arrow>
						<ListItemButton
							className={styles.listItemButton}
							onClick={() => dispatch(reset())}
						>
							<ListItemIcon
								className={`${styles.activeStyleButton} ${styles.listItemIcon}`}
							>
								<Img className={styles.img} />
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
										<Img className={`${styles.img} ${styles.imgColor}`} />
									) : (
										<Img className={styles.img} />
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
