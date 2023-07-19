import { Box, Divider, List } from '@mui/material'
import { Navigate, Outlet } from 'react-router-dom'
import ButtonListItem from '../../components/layout/ButtonListItem/ButtonListItem'
import LogoListItem from '../../components/layout/LogoListItem/LogoListItem'
import { useGetUserData } from '../../hooks/userDataHook'
import styles from './Layout.module.scss'

const Layout = () => {
	if (!useGetUserData()) {
		return <Navigate to='/login' />
	}

	return (
		<Box className={styles.container}>
			<Box component='nav' className={styles.nav}>
				<List>
					<LogoListItem pathImg='/logo.svg' />
					<Divider variant='middle' className={styles.divider} />
					<ButtonListItem
						pathImg='/document.svg'
						to='release'
						label='Выпускаемые процессы'
					/>
					<ButtonListItem
						pathImg='/confirmation.svg'
						to='approval'
						label='Этапы на согласование'
					/>
					<ButtonListItem
						pathImg='/analysis.svg'
						to='analysis'
						label='Аналитика'
						otherPage='http://localhost:3000'
					/>
					<ButtonListItem
						pathImg='/key.svg'
						to='test'
						label='Страница админа'
					/>
				</List>
				<List>
					<Divider variant='middle' className={styles.divider} />
					<ButtonListItem
						pathImg='/notification.svg'
						to='test'
						label='Уведомления'
					/>
					<ButtonListItem pathImg='/settings.svg' to='test' label='Настройки' />
					<ButtonListItem
						pathImg='/user1.svg'
						to='test'
						label='Страница пользователя'
					/>
				</List>
			</Box>
			<Outlet />
		</Box>
	)
}

export default Layout
