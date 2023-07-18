import { Box, Tooltip, Typography } from '@mui/material'
import { FC, memo } from 'react'
import { IUserFieldProps } from '../../../../../interfaces/IMainPage/ISelectedProcess/IInfoProcess/IUserFieldProps/IUserFieldProps'
import styles from '/src/styles/MainPageStyles/SelectedProcessStyles/InfoProcessStyles/UserFieldStyles/UserField.module.scss'

const UserField: FC<IUserFieldProps> = memo(({ group, responsible, role }) => {
	return (
		<Box className={styles.userField}>
			<Box className={styles.wrapIcon}>
				<img className={styles.icon} src='/user2.svg' />
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

export default UserField
