import { Box, Tooltip, Typography } from '@mui/material'
import { FC, memo } from 'react'
import { ReactComponent as User } from '/src/assets/user1.svg'
import styles from './UserInfo.module.scss'

interface UserInfoProps {
	group: string
	responsible: string
	role: string
}

const UserInfo: FC<UserInfoProps> = memo(({ group, responsible, role }) => {
	return (
		<Box className={styles.userField}>
			<Box className={styles.wrapIcon}>
				<User className={styles.icon} />
			</Box>
			<Box className={styles.wrapUserInfo}>
				<Typography variant='h6' className={styles.title}>
					{role}
				</Typography>
				<Box className={styles.descriptions}>
					<Typography className={styles.responsible}>{responsible}</Typography>
					<Box className={styles.wrapGroup}>
						{group && group.length > 40 ? (
							<Tooltip title={group} arrow>
								<Typography variant='body2' className={styles.group}>
									{group.slice(0, 40)}...
								</Typography>
							</Tooltip>
						) : (
							<Typography variant='body2' className={styles.group}>
								{group}
							</Typography>
						)}
					</Box>
				</Box>
			</Box>
		</Box>
	)
})

export default UserInfo
