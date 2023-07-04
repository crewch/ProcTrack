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
					<ButtonListItem src='/document.svg' to='/' />
					<ButtonListItem src='/confirmation.svg' to='confirmation' />
					<ButtonListItem src='/analysis.svg' to='analysis' />
					<ButtonListItem src='/key.svg' to='key' />
				</List>
				<List>
					<Divider variant='middle' className={styles.divider} />
					<ButtonListItem src='/notification.svg' to='test4' />
					<ButtonListItem src='/settings.svg' to='test5' />
					<ButtonListItem src='/user1.svg' to='test6' />
				</List>
			</Box>
			<Outlet />
		</Box>
	)
}

export default Layout
