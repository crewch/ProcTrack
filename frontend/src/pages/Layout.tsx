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

const Layout = () => {
	return (
		<Box sx={{ height: '100%', display: 'flex' }}>
			<Box
				component='nav'
				sx={{
					height: '100%',
					width: '86px',
					backgroundColor: 'white',
					display: 'flex',
					flexDirection: 'column',
					justifyContent: 'space-between',
				}}
			>
				<List>
					<LogoListItem src={Logo} />
					<Divider variant='middle' sx={{ borderWidth: 1 }} />
					<ButtonListItem src={Document} to='/' />
					<ButtonListItem src={Confirmation} to='test1' />
					<ButtonListItem src={Analysis} to='test2' />
					<ButtonListItem src={Key} to='test3' />
				</List>
				<List>
					<Divider variant='middle' sx={{ borderWidth: 1 }} />
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
