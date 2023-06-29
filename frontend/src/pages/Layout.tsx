import { Box, Divider, List } from '@mui/material'
import { Outlet } from 'react-router-dom'
import Logo from '/logo.svg'
import Document from '../assets/document.svg'
import Confirmation from '../assets/confirmation.svg'
import Analysis from '../assets/analysis.svg'
import Key from '../assets/key.svg'
import Notifications from '../assets/notification.svg'
import Settings from '../assets/settings.svg'
import User1 from '../assets/user1.svg'
import ButtonListItem from '../components/Layout/ButtonListItem'
import LogoListItem from '../components/Layout/LogoListItem'
import styles from '../styles/LayoutStyles/Layout.module.scss'

const Layout = () => {
	return (
		<Box className={styles.wrap}>
			<Box component='nav' className={styles.nav}>
				<List>
					<LogoListItem src={Logo} />
					<Divider variant='middle' className={styles.divider} />
					<ButtonListItem src={Document} to='/' />
					<ButtonListItem src={Confirmation} to='confirmation' />
					<ButtonListItem src={Analysis} to='analysis' />
					<ButtonListItem src={Key} to='key' />
				</List>
				<List>
					<Divider variant='middle' className={styles.divider} />
					<ButtonListItem src={Notifications} to='test4' />
					<ButtonListItem src={Settings} to='test5' />
					<ButtonListItem src={User1} to='test6' />
				</List>
			</Box>
			<Outlet />
		</Box>
	)
}

export default Layout
