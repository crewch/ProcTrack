import { Box, Divider, List } from '@mui/material'
import { Outlet } from 'react-router-dom'
import ButtonListItem from '../components/Layout/ButtonListItem'
import LogoListItem from '../components/Layout/LogoListItem'
import styles from '../styles/LayoutStyles/Layout.module.scss'

const Layout = () => {
	return (
		<Box className={styles.wrap}>
			<Box component='nav' className={styles.nav}>
				<List>
					<LogoListItem src='/logo.svg' />
					<Divider variant='middle' className={styles.divider} />
					<ButtonListItem
						src='/document.svg'
						to='/'
						lable='Выпускаемые процессы'
					/>
					<ButtonListItem
						src='/confirmation.svg'
						to='confirmation'
						lable='Этапы на согласование'
					/>
					<ButtonListItem
						src='/analysis.svg'
						to='analysis'
						otherPage
						lable='Аналитика'
					/>
					<ButtonListItem
						src='/key.svg'
						to='/http://localhost:3000'
						lable='Страница админа'
					/>
				</List>
				<List>
					<Divider variant='middle' className={styles.divider} />
					<ButtonListItem
						src='/notification.svg'
						to='test4'
						lable='Уведомления'
					/>
					<ButtonListItem src='/settings.svg' to='test5' lable='Настройки' />
					<ButtonListItem
						src='/user1.svg'
						to='test6'
						lable='Страница пользователя'
					/>
				</List>
			</Box>
			<Outlet />
		</Box>
	)
}

export default Layout
