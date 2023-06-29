import { Box, Typography } from '@mui/material'
import { FC } from 'react'
import styles from '/src/styles/MainPageStyles/SelectedProcessStyles/InfoProcessStyles/UserFieldStyles/UserField.module.scss'

const UserField: FC<{ group: string; responsible: string }> = ({
	group,
	responsible,
}) => {
	return (
		<Box className={styles.userField}>
			<Box className={styles.wrapIcon}>
				<img className={styles.icon} src='/src/assets/user2.svg' />
			</Box>
			<Box className={styles.wrapUserInfo}>
				<Typography variant='h6' className={styles.title}>
					Ответственный
				</Typography>
				<Box className={styles.descriptions}>
					<Typography variant='body1' className={styles.responsible}>
						{responsible}
					</Typography>
					<Box className={styles.wrapGroup}>
						<Typography variant='body2' className={styles.group}>
							{group}
						</Typography>
					</Box>
				</Box>
			</Box>
		</Box>
	)
}

export default UserField
